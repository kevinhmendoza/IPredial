using Core.Entities.General;
using Core.UseCase.Contracts;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
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
                TipoFiltro = request.TipoFiltro
            });

            return Task.FromResult(new ConsultarEstadoCuentaInteractorResponse(validationResult, listEstadoCuenta.EstadoCuenta));
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
        public List<ConsultarEstadoCuentaSistemaLocalServiceModelView> EstadoCuenta { get; set; }
        public ConsultarEstadoCuentaInteractorResponse(ValidationResult validationResult, List<ConsultarEstadoCuentaSistemaLocalServiceModelView> estadoCuenta)
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


}
