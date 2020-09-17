using Core.Entities.General;
using Core.UseCase.Contracts;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.UseCase.Dtos.General;

namespace Core.UseCase.General
{
    public class ConsultarTerceroByIdentificacionInteractor : IRequestHandler<ConsultarTerceroByIdentificacionRequest, ConsultarTerceroByIdentificacionResponse>
    {
        private readonly IConsultarTerceroByIdentificacionRequestValidator<ConsultarTerceroByIdentificacionRequest> _validator;
        private readonly IMapper _mapper;

        public ConsultarTerceroByIdentificacionInteractor(IConsultarTerceroByIdentificacionRequestValidator<ConsultarTerceroByIdentificacionRequest> validator, IMapper mapper)
        {
            _validator = validator;
            _mapper = mapper;
        }

        public Task<ConsultarTerceroByIdentificacionResponse> Handle(ConsultarTerceroByIdentificacionRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new ConsultarTerceroByIdentificacionResponse(validationResult));

            Tercero TerceroConsultar = _validator.GetTercero();

            TerceroDto Tercero = new TerceroDto();

            _mapper.Map(TerceroConsultar, Tercero);

            return Task.FromResult(new ConsultarTerceroByIdentificacionResponse(validationResult,Tercero));
        }
    }

    public class ConsultarTerceroByIdentificacionRequest : IRequest<ConsultarTerceroByIdentificacionResponse>
    {
        public string Identificacion { get; set; }
    }

    public class ConsultarTerceroByIdentificacionResponse
    {
        public ValidationResult ValidationResult { get; }
        public TerceroDto Tercero { get; set; }
        public ConsultarTerceroByIdentificacionResponse(ValidationResult validationResult, TerceroDto tercero)
        {
            Tercero = tercero;
            ValidationResult = validationResult;
        }
        public ConsultarTerceroByIdentificacionResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class ConsultarTerceroByIdentificacionRequestValidator : AbstractValidator<ConsultarTerceroByIdentificacionRequest>, IConsultarTerceroByIdentificacionRequestValidator<ConsultarTerceroByIdentificacionRequest>
    {
        private readonly ITerceroRepository _terceroRepository;
        private Tercero _tercero;
        public ConsultarTerceroByIdentificacionRequestValidator(ITerceroRepository terceroRepository)
        {
            _terceroRepository = terceroRepository;
            RuleFor(r => r.Identificacion).NotEmpty().WithMessage("Debe especificar el número de identificación del tercero");
            RuleFor(t => TerceroExiste(t.Identificacion)).NotNull().WithMessage("No se encontro el tercero").When(t => !string.IsNullOrEmpty(t.Identificacion));
        }

        public Tercero GetTercero()
        {
            return _tercero;
        }

        private Tercero TerceroExiste(string identificacion)
        {
            _tercero= _terceroRepository.FindFirstOrDefault(t=>t.Identificacion==identificacion);
            return _tercero;
        }
    }

    public interface IConsultarTerceroByIdentificacionRequestValidator<in T>
    {
        ValidationResult Validate(T instance);
        Tercero GetTercero();
    }
}
