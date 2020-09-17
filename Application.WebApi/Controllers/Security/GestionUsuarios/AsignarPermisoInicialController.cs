using Infrastructure.Security.Interactors;
using System.Web.Http;
using Infrastructure.Initialization;
using System.Collections.Generic;
using MediatR;
using Core.UseCase.Contracts;
using System.Threading.Tasks;
using Application.WebApi.ViewModels.Security.GestionUsuarios;
using Core.UseCase.Security.GestionTercero;
using Application.WebApi.Extensions;
using System.Linq;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [RoutePrefix("api/Permission/Initial")]
    [Authorize(Roles = "SEGURIDADAssignIndexPageToUser")]
    public class AsignarPermisoInicialController : ApiController { 

        private readonly IMediator _mediator = null;
        private readonly IMapper _mapper = null;
        public AsignarPermisoInicialController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("{TerceroId}")]
        public TerceroWithUserAndPermission GetTerceroWithUser(int TerceroId)
        {
            TerceroWithUserAndPermission _response = new TerceroWithUserAndPermission();
            Task<ConsultarTerceroResponse> _responseInteractorTercero = _mediator.Send(new ConsultarTerceroRequest() { TerceroId = TerceroId });
            if (!_responseInteractorTercero.Result.ValidationResult.IsValid)
            {
                _response.EstablecerError(_responseInteractorTercero.Result.ValidationResult.ToText());
            }
            else
            {
                ConsultarUserByTerceroIdInteractor _interactorUser = new ConsultarUserByTerceroIdInteractor();
                ConsultarUserByTerceroIdResponse _responseInteractorUser = _interactorUser.ConsultarUsuario(TerceroId);
                if (_responseInteractorUser.Error)
                {
                    _response.EstablecerError(_responseInteractorUser.Mensaje);
                }
                else
                {
                    ConsultarPermisosInteractor _request = new ConsultarPermisosInteractor();
                    ConsultarPermisosResponse _responseGetModulos =_request.GetRoles(_responseInteractorUser.Usuario.UserName);
                    
                    _mapper.Map(_responseInteractorTercero.Result.Tercero, _response.Tercero);
                    _mapper.Map(_responseInteractorUser.Usuario, _response.Usuario);

                    _responseGetModulos.Modulos.ForEach(t =>
                    {
                        _response.Permisos.AddRange(t.SubMenu.Where(x=>x.Habilitado).ToList());
                    });
                    _response.PermisoInicial = _responseGetModulos.PermisoInicial;
                }
            }
            return _response;
        }

        [Route("{UserName}/{permisoInicial}")]
        public AsignarPermisoInitialResponse PostRegistrarPermisoInicial(string UserName, string permisoInicial)
        {
            AsignarPermisoInitialInteractor _interactor = new AsignarPermisoInitialInteractor();
            return _interactor.AsignarPermisoInicial(UserName,permisoInicial);
        }
    }
}
