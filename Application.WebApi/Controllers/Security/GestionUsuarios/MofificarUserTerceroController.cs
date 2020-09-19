using Application.WebApi.Extensions;
using Core.UseCase.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using Infrastructure.Security.Interactors;
using Infrastructure.Security;
using Core.UseCase.Security.GestionTercero;
using Application.WebApi.ViewModels.Security.GestionUsuarios;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [RoutePrefix("api/Modificar/UserWithTercero")]
    [Authorize(Roles = "Seguridad.ModifiedTerceroAndUser")]
    public class MofificarUserTerceroController : ApiController
    {
        private readonly IMediator _mediator = null;
        private readonly IMapper _mapper = null;
        public MofificarUserTerceroController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("{TerceroId}")]
        public TerceroWithUser GetTerceroWithUser(int TerceroId)
        {
            TerceroWithUser _response = new TerceroWithUser();
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
                    _mapper.Map(_responseInteractorTercero.Result.Tercero, _response.Tercero);
                    _mapper.Map(_responseInteractorUser.Usuario, _response.Usuario);
                }
            }
            return _response;
        }

        [Route("")]
        public ModificarTerceroWithUserResponse PostModificarTercero(ModificarTerceroWithUserRequest _request)
        {
            ModificarTerceroRequest _requestInteractor = new ModificarTerceroRequest();

            _mapper.Map(_request, _requestInteractor);

            ModificarTerceroWithUserResponse _response = new ModificarTerceroWithUserResponse();
            Task<ModificarTerceroResponse> _responseInteractor = _mediator.Send(_requestInteractor);
            if (!_responseInteractor.Result.ValidationResult.IsValid)
            {
                _response.EstablecerError(_responseInteractor.Result.ValidationResult.ToText());
            }
            else
            {
                _response.Mensaje = _responseInteractor.Result.Mensaje;
                ModificarDatosTerceroUserInteractor _interactorModificarUser = new ModificarDatosTerceroUserInteractor();
                ModificarDatosTerceroUserResponse _responseModificarUser = _interactorModificarUser.ModificarUsuario(_request.TerceroId, _responseInteractor.Result.Tercero.NombreCompleto, _responseInteractor.Result.Tercero.Identificacion, _request.UserName,_request.Email);
                if (_responseModificarUser.Error)
                {
                    _response.EstablecerError(_responseModificarUser.Mensaje);
                }else
                {
                    _response.Mensaje += _responseModificarUser.Mensaje;
                }
            }
            return _response;
        }
    }
}
