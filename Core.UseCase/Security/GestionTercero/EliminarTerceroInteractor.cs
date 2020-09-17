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

namespace Core.UseCase.Security.GestionTercero
{
    public class EliminarTerceroInteractor : IRequestHandler<EliminarTerceroRequest, EliminarTerceroResponse>,IInteractor
    {
        public string Module => ModulesEnumeration.Seguridad.Value; 
        public string Name => GetType().Name; 

        private readonly ITerceroRepository _terceroRepository;
        private readonly IEliminarTerceroRequestValidator<EliminarTerceroRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public EliminarTerceroInteractor(IEliminarTerceroRequestValidator<EliminarTerceroRequest> validator, ITerceroRepository repository, IMapper mapper, IUnitOfWork iUnitOfWork)
        {
            _validator = validator;
            _terceroRepository = repository;
            _mapper = mapper;
            _iUnitOfWork = iUnitOfWork;
        }


        public Task<EliminarTerceroResponse> Handle(EliminarTerceroRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new EliminarTerceroResponse(validationResult));

            Tercero TerceroEliminar = _validator.GetTercero();

            string _mensaje = $"Se elimino el tercero {TerceroEliminar.Nombres} {TerceroEliminar.Apellidos} correctamente!";

            _terceroRepository.Delete(TerceroEliminar);

            _iUnitOfWork.Commit(this);

            return Task.FromResult(new EliminarTerceroResponse(validationResult,_mensaje));
        }
    }

    public class EliminarTerceroRequest : IRequest<EliminarTerceroResponse>
    {
        public int TerceroId { get; set; }
    }

    public class EliminarTerceroResponse
    {
        public ValidationResult ValidationResult { get; }
        public string Mensaje{ get; }
        public EliminarTerceroResponse(ValidationResult validationResult, string mensaje)
        {
            Mensaje = mensaje;
            ValidationResult = validationResult;
        }
        public EliminarTerceroResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class EliminarTerceroRequestValidator : AbstractValidator<EliminarTerceroRequest>, IEliminarTerceroRequestValidator<EliminarTerceroRequest>
    {
        private readonly ITerceroRepository _terceroRepository;
        private Tercero _tercero;
        public EliminarTerceroRequestValidator(ITerceroRepository terceroRepository)
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

    public interface IEliminarTerceroRequestValidator<in T>
    {
        ValidationResult Validate(T instance);
        Tercero GetTercero();
    }
}
