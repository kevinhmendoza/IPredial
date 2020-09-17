using Application.WebApi.Extensions;
using Core.UseCase.Contracts;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using Core.UseCase.General;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [RoutePrefix("api/Tercero")]
    [Authorize(Roles = "SEGURIDADModifiedTerceroAndUser")]
    public class GetTerceroByIdentificacionController : ApiController
    {
        private readonly IMediator _mediator = null;
        private readonly IMapper _mapper = null;
        public GetTerceroByIdentificacionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("{Identificacion}")]
        public IHttpActionResult GetTercero(string identificacion)
        {
            Task<ConsultarTerceroByIdentificacionResponse> _responseInteractorTercero = _mediator.Send(new ConsultarTerceroByIdentificacionRequest() {Identificacion=identificacion});
            if (!_responseInteractorTercero.Result.ValidationResult.IsValid)
            {
                return BadRequest( _responseInteractorTercero.Result.ValidationResult.ToText());
            }
            return Ok(_responseInteractorTercero.Result);
        }
    }
}
