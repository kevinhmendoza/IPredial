using Core.Entities.General;
using Core.UseCase.Contracts;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.UseCase.Dtos.General;
using System.Collections.Generic;
using Core.Entities.Contracts;

namespace Core.UseCase.General
{
    public class ConsultarEstadoCuentaInteractor : IRequestHandler<ConsultarEstadoCuentaInteractorRequest, ConsultarEstadoCuentaInteractorResponse>
    {
        private readonly IValidator<ConsultarEstadoCuentaInteractorRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IConsultarEstadoCuentaSistemaLocalService _consultarEstadoCuentaSistemaLocalService;
        public ConsultarEstadoCuentaInteractor(IValidator<ConsultarEstadoCuentaInteractorRequest> validator, IMapper mapper, IConsultarEstadoCuentaSistemaLocalService consultarEstadoCuentaSistemaLocalService)
        {
            _validator = validator;
            _mapper = mapper;
            _consultarEstadoCuentaSistemaLocalService = consultarEstadoCuentaSistemaLocalService;
        }

        public Task<ConsultarEstadoCuentaInteractorResponse> Handle(ConsultarEstadoCuentaInteractorRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new ConsultarEstadoCuentaInteractorResponse(validationResult));

            var listEstadoCuenta = _consultarEstadoCuentaSistemaLocalService.ConsultarEstadoCuenta(new ConsultarEstadoCuentaSistemaLocalServiceRequest
            {

                Filtro = request.Filtro,
                TipoFiltro = request.Filtro
            });

            List<ConsultarEstadoCuentaInteractorRequestModelView> EstadoCuenta = new List<ConsultarEstadoCuentaInteractorRequestModelView>();

            _mapper.Map(listEstadoCuenta, EstadoCuenta);

            return Task.FromResult(new ConsultarEstadoCuentaInteractorResponse(validationResult, EstadoCuenta));
        }
    }

    public class ConsultarEstadoCuentaInteractorRequest : IRequest<ConsultarEstadoCuentaInteractorResponse>
    {
        public string TipoFiltro { get; set; }
        public string Filtro { get; set; }
    }

    public class ConsultarEstadoCuentaInteractorResponse
    {
        public ValidationResult ValidationResult { get; }
        public List<ConsultarEstadoCuentaInteractorRequestModelView> EstadoCuenta { get; set; }
        public ConsultarEstadoCuentaInteractorResponse(ValidationResult validationResult, List<ConsultarEstadoCuentaInteractorRequestModelView> estadoCuenta)
        {
            EstadoCuenta = estadoCuenta;
            ValidationResult = validationResult;
        }
        public ConsultarEstadoCuentaInteractorResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class ConsultarEstadoCuentaInteractorRequestValidator : AbstractValidator<ConsultarEstadoCuentaInteractorRequest>
    {

        public ConsultarEstadoCuentaInteractorRequestValidator()
        {

            RuleFor(r => r.TipoFiltro).NotEmpty().WithMessage("Debe especificar el tipo de filtro");
            RuleFor(r => r.Filtro).NotEmpty().WithMessage("Debe especificar el filtro");

        }


    }

    public class ConsultarEstadoCuentaInteractorRequestModelView
    {
        public string ReferenciaCatastral { get; set; }
        public string IdentifiacionPropietario { get; set; }
        public string Propietario { get; set; }
        public string Direccion { get; set; }
        public double Avaluo { get; set; }
        public decimal AreaTerreno { get; set; }
        public decimal AreaConstruida { get; set; }
        public string Clase { get; set; }
        public int Estrato { get; set; }
        public string DestinoEconomico { get; set; }
        public string UsoSuelo { get; set; }
        public string NumeroLiquidacion { get; set; }
        public int Vigencia { get; set; }
        public int Periodo { get; set; }
        public double ValorCapital { get; set; }
        public double ValorInteres { get; set; }
        public double Total => ValorCapital + ValorInteres;
    }


}
