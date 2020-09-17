using System.Web.Http;
using System.Collections.Generic;
using MediatR;
using Core.UseCase.Contracts;
using System.Threading.Tasks;
using Application.WebApi.Extensions;
using Core.UseCase.Security.Auditoria;
using Application.WebApi.ViewModels.Security.Auditoria;

namespace Application.WebApi.Controllers.Security.Auditoria
{
    [Authorize(Roles = "SEGURIDADConsultaAuditoria")]
    [RoutePrefix("api/Auditoria")]
    public class AuditoriaController : ApiController
    {
        private readonly IMediator _mediator = null;
        private readonly IMapper _mapper = null;
        public AuditoriaController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("datos")]
        public IHttpActionResult ConsultarDatosFormulario()
        {
            ConfiguracionAuditoriaRequest request = new ConfiguracionAuditoriaRequest();
            Task<ConfiguracionAuditoriaResponse> response = _mediator.Send(request);
            DatosAuditoriaViewsModel parametros = new DatosAuditoriaViewsModel();
            parametros.Acciones = response.Result.Acciones;
            parametros.Modulos = response.Result.Modulos;
            parametros.Tablas = response.Result.TableName;
            return Ok(parametros);
        }


        [HttpPost]
        [Route("Parametrizada")]
        public IHttpActionResult ConsultaParametrizada(ConsultaAuditoriaRequest viewRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.ToText());
            Task<ConsultaAuditoriaResponse> responseRemisionarOrdenesPago = _mediator.Send(viewRequest);
            if (responseRemisionarOrdenesPago.Result.ValidationResult.IsValid)
            {
                ConsultaAuditoriaResponseViewModel viewResponse = new ConsultaAuditoriaResponseViewModel();
                _mapper.Map(responseRemisionarOrdenesPago.Result.Encontrados, viewResponse.Auditorias);
                return Ok(viewResponse);
            }
            else return BadRequest(responseRemisionarOrdenesPago.Result.ValidationResult.ToText());
        }

        [HttpGet]
        [Route("TablaName/{TablaName}/RecordID/{RecordID}")]
        public IHttpActionResult Detalles(string TablaName, string RecordID)
        {
            ConsultaDetalleAuditoriaRequest request = new ConsultaDetalleAuditoriaRequest()
            {
                RecordID = RecordID,
                Tabla = TablaName,
            };
            Task<ConsultaDetalleAuditoriaResponse> response = _mediator.Send(request);
            if (response.Result.ValidationResult.IsValid)
            {
                return Ok(response.Result.Encontrados);
            }
            else return BadRequest(response.Result.ValidationResult.ToText());
        }
    }
}
