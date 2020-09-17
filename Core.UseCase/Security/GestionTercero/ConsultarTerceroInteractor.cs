using Core.Entities.General;
using Core.UseCase.Contracts;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.UseCase.Base;
using Core.Entities.Enumerations.General;
using Core.UseCase.Util;
using System;
using Core.UseCase.Dtos.General;

namespace Core.UseCase.Security.GestionTercero
{
    public class ConsultarTerceroInteractor : IRequestHandler<ConsultarTerceroRequest, ConsultarTerceroResponse>
    {
        private readonly ITerceroRepository _terceroRepository;
        private readonly IConsultarTerceroRequestValidator<ConsultarTerceroRequest> _validator;
        private readonly IMapper _mapper;

        public ConsultarTerceroInteractor(IConsultarTerceroRequestValidator<ConsultarTerceroRequest> validator, ITerceroRepository repository, IMapper mapper)
        {
            _validator = validator;
            _terceroRepository = repository;
            _mapper = mapper;
        }


        public Task<ConsultarTerceroResponse> Handle(ConsultarTerceroRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new ConsultarTerceroResponse(validationResult));

            Tercero TerceroConsultar = _validator.GetTercero();

            TerceroDto Tercero = new TerceroDto();

            _mapper.Map(TerceroConsultar, Tercero);

            return Task.FromResult(new ConsultarTerceroResponse(validationResult,Tercero));
        }
    }

    public class ConsultarTerceroRequest : IRequest<ConsultarTerceroResponse>
    {
        public int TerceroId { get; set; }
    }

    public class ConsultarTerceroResponse
    {
        public ValidationResult ValidationResult { get; }
        public TerceroDto Tercero { get; set; }
        public ConsultarTerceroResponse(ValidationResult validationResult, TerceroDto tercero)
        {
            Tercero = tercero;
            ValidationResult = validationResult;
        }
        public ConsultarTerceroResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class ConsultarTerceroRequestValidator : AbstractValidator<ConsultarTerceroRequest>, IConsultarTerceroRequestValidator<ConsultarTerceroRequest>
    {
        private readonly ITerceroRepository _terceroRepository;
        private Tercero _tercero;
        public ConsultarTerceroRequestValidator(ITerceroRepository terceroRepository)
        {
            _terceroRepository = terceroRepository;
            RuleFor(t => TerceroExiste(t.TerceroId)).NotNull().WithMessage("No se encontro el tercero").When(t => t.TerceroId >0);
        }

        public Tercero GetTercero()
        {
            return _tercero;
        }

        private Tercero TerceroExiste(int terceroId)
        {
            _tercero= _terceroRepository.Find(terceroId);
            return _tercero;
        }
    }

    public interface IConsultarTerceroRequestValidator<in T>
    {
        ValidationResult Validate(T instance);
        Tercero GetTercero();
    }
}
