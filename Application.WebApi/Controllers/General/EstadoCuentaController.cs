using Application.WebApi.Extensions;
using AutoMapper;
using Core.UseCase.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Application.WebApi.Controllers.General
{
    [RoutePrefix("api/EstadoCuenta")]
    [AllowAnonymous]
    public class EstadoCuentaController : ApiController
    {
        private readonly IMediator _mediator = null;
        private readonly IMapper _mapper = null;
        public EstadoCuentaController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("Consultar")]
        [HttpPost]
        public IHttpActionResult PostConsultarEstadoCuenta(ConsultarEstadoCuentaInteractorRequest request)
        {
            Task<ConsultarEstadoCuentaInteractorResponse> _responseInteractorConsultarEstadoCuenta = _mediator.Send(request);
            if (!_responseInteractorConsultarEstadoCuenta.Result.ValidationResult.IsValid)
            {
                return BadRequest(_responseInteractorConsultarEstadoCuenta.Result.ValidationResult.ToText());
            }
            return Ok(_responseInteractorConsultarEstadoCuenta.Result);
        }
    }
}
