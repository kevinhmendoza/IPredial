using Core.UseCase.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Application.WebApi.ViewModels.General;
using Infrastructure.Security.Interactors;
using Application.WebApi.Extensions;
using Infrastructure.Security;
using Infrastructure.Initialization;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using Core.UseCase.Security.GestionTercero;
using Infrastructure.System;
using Infrastructure.Security.Services;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [Authorize]
    [RoutePrefix("api/Gestion/User")]
    public class GestionUserController : ApiController
    {
        private readonly IMediator _mediator = null;
        private readonly IMapper _mapper = null;
        private ApplicationUserManager _userManager;
        public GestionUserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "SEGURIDADPermisosToUser,SEGURIDADChangePasswordToUser,SEGURIDADForzeEmailConfirmToUser")]
        [Route("{userName}")]
        public IHttpActionResult Get(string userName)
        {
            ConsultarUserInteractor _interactor = new ConsultarUserInteractor();

            ConsultarUserResponse _rersponse = _interactor.ConsultarUsuario(userName);

            if (!_rersponse.Error)
            {
                return Ok(_rersponse);
            }

            return BadRequest(_rersponse.Mensaje);

        }

        [Route("Register")]
        [Authorize(Roles = "SEGURIDADGestionUsuarios")]
        public IHttpActionResult Post(RegisterUserViewModel usuarioRegistrar)
        {
            RegistrarTerceroRequest _request = new RegistrarTerceroRequest();

            usuarioRegistrar.Tercero.CorreoElectronico = usuarioRegistrar.Usuario.Email;

            _mapper.Map(usuarioRegistrar.Tercero,_request);

            Task<RegistrarTerceroResponse> _responseInteractoRegistrar = _mediator.Send(_request);

            if (!_responseInteractoRegistrar.Result.ValidationResult.IsValid)
            {
                return BadRequest(_responseInteractoRegistrar.Result.ValidationResult.ToText());
            }

            usuarioRegistrar.Usuario.Identificacion = _responseInteractoRegistrar.Result.Tercero.Identificacion;
            usuarioRegistrar.Usuario.NombreCompleto= _responseInteractoRegistrar.Result.Tercero.NombreCompleto;
            usuarioRegistrar.Usuario.TerceroId = _responseInteractoRegistrar.Result.Tercero.Id;
            usuarioRegistrar.Usuario.FechaDesactivacion = new DateTime(ByADateTime.Now.Year,12,31);

            RegisterUserResponse response = RegistrarUsuarioOwin(usuarioRegistrar);

            if (!response.Error)
            {
                return Ok(response);
            }
            else
            {
                Task<EliminarTerceroResponse> _responseInteractorEliminar = _mediator.Send(new EliminarTerceroRequest() { TerceroId=_responseInteractoRegistrar.Result.Tercero.Id});
                if (!_responseInteractorEliminar.Result.ValidationResult.IsValid)
                {
                    return BadRequest(_responseInteractorEliminar.Result.ValidationResult.ToText());
                }
                return BadRequest($"No se pudo registrar el usuario, {response.Mensaje}"); 
            }

        }

        [Route("ChangeState")]
        public IHttpActionResult PostCambiarEstadoTerceroAndUser(ChangeStateUserRequest _request)
        {
            ChangeStateUserInteractor interactor = new ChangeStateUserInteractor();
            var response=interactor.Inactive(_request);
            if (response.Error)
            {
                return BadRequest(response.Mensaje);
            }

            return Ok(response);
        }

        private RegisterUserResponse RegistrarUsuarioOwin(RegisterUserViewModel usuarioRegistrar)
        {
            RegisterUserResponse response = new RegisterUserResponse();
            try
            {
                RegistreUserInteractor gu = new RegistreUserInteractor(this.UserManager);
                var url = new Uri(Url.Link("ConfirmEmailRoute", new { userId = "user.Id", code = "codigo" }));
                string URL_CONFIRMAR = url.AbsoluteUri;

                Usuario _userRegister = new Usuario();

                _mapper.Map(usuarioRegistrar.Usuario, _userRegister);

                response = gu.CreateUser(_userRegister, URL_CONFIRMAR);

            }
            catch (Exception ex)
            {
                response.EstablecerError($"{ex.Message}");
            }
            return response;
        }

        [Route("Roles")]
        public ConsultarPermisosResponse GetRoles()
        {
            ConsultarPermisosInteractor _request = new ConsultarPermisosInteractor();
            return _request.GetRoles();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("~/public/api/Usuarios/ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }
            ConfirmEmailnteractor gu = new ConfirmEmailnteractor(this.UserManager);
            IdentityResult result = await gu.ConfirmedEmail(userId, code);
            string Host = HttpContext.Current.Request.Url.Host;
            int Port = HttpContext.Current.Request.Url.Port;
            if (result.Succeeded)
            {
                return Redirect(string.Format("http://{0}:{1}", Host, Port) + "/Templates/MailConfirmed.html");
            }
            else if (!result.Succeeded)
            {
                return Redirect(string.Format("http://{0}:{1}", Host, Port) + "/Templates/MailNoConfirmed.html");
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
