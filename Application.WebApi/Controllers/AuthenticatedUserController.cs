using Core.Entities.Contracts;
using Infrastructure.Initialization;
using Infrastructure.Security;
using Infrastructure.Security.Interactors;
using System.Web.Http;

namespace Application.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class AuthenticatedUserController : ApiController
    {
        private readonly ISistema _sistema;
        public AuthenticatedUserController()
        {
            _sistema = new Sistema();
        }

        [Route("ChangePassword")]
        public CambiarPasswordResponse PostCambiarPasssword(CambiarPasswordRequest request)
        {
            CambiarPasswordInteractor _interactor = new CambiarPasswordInteractor();
            request.UserName = _sistema.UserName;
            return _interactor.CambiarPassword(request);
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            ConsultarUserInteractor _interactor = new ConsultarUserInteractor();

            ConsultarUserResponse _rersponse = _interactor.ConsultarUsuario(_sistema.UserName);

            if (!_rersponse.Error)
            {
                return Ok(_rersponse);
            }

            return BadRequest(_rersponse.Mensaje);
        }

        [Route("Permission")]
        public ConsultarPermisosResponse GetRoles()
        {
            ConsultarPermisosInteractor _request = new ConsultarPermisosInteractor();
            return _request.GetRoles();
        }
    }
}
