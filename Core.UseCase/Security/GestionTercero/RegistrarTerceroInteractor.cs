using Core.Entities.General;
using Core.UseCase.Contracts;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.UseCase.Dtos.General;
using Core.UseCase.Base;
using Core.Entities.Enumerations.General;
using Core.UseCase.Util;

namespace Core.UseCase.Security.GestionTercero
{
    public class RegistrarTerceroInteractor : IRequestHandler<RegistrarTerceroRequest, RegistrarTerceroResponse>, IInteractor
    {
        public string Module => ModulesEnumeration.Seguridad.Value;
        public string Name => GetType().Name;

        private readonly ITerceroRepository _terceroRepository;
        private readonly IValidator<RegistrarTerceroRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public RegistrarTerceroInteractor(IValidator<RegistrarTerceroRequest> validator, ITerceroRepository repository, IMapper mapper, IUnitOfWork iUnitOfWork)
        {
            _validator = validator;
            _terceroRepository = repository;
            _mapper = mapper;
            _iUnitOfWork = iUnitOfWork;
        }


        public Task<RegistrarTerceroResponse> Handle(RegistrarTerceroRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new RegistrarTerceroResponse(validationResult));

            Tercero _TerceroGuardar = new Tercero();

            _mapper.Map<RegistrarTerceroRequest, Tercero> (request, _TerceroGuardar);

            _terceroRepository.Add(_TerceroGuardar);

            _iUnitOfWork.Commit(this);

            TerceroDto TerceroDto = new TerceroDto();

            _mapper.Map<Tercero,TerceroDto>(_TerceroGuardar, TerceroDto);

            return Task.FromResult(new RegistrarTerceroResponse(validationResult, TerceroDto));
        }
    }

    public class RegistrarTerceroRequest : IRequest<RegistrarTerceroResponse>
    {
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
    }

    public class RegistrarTerceroResponse
    {
        public ValidationResult ValidationResult { get; }
        public TerceroDto Tercero { get; }
        public RegistrarTerceroResponse(ValidationResult validationResult, TerceroDto Personas)
        {
            Tercero = Personas;
            ValidationResult = validationResult;
        }
        public RegistrarTerceroResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class RegistrarTerceroRequestValidator : AbstractValidator<RegistrarTerceroRequest>
    {
        private readonly ITerceroRepository _terceroRepository;
        public RegistrarTerceroRequestValidator(ITerceroRepository terceroRepository)
        {
            _terceroRepository = terceroRepository;
            RuleFor(r => r.Identificacion).NotEmpty().WithMessage("Debe especificar el número de identificación del tercero");
            RuleFor(r => r.Nombres).NotEmpty().WithMessage("Debe especificar los nombres del tercero");
            RuleFor(r => r.Apellidos).NotEmpty().WithMessage("Debe especificar los apellidos del tercero");
            RuleFor(r => r.Direccion).NotEmpty().WithMessage("Debe especificar la dirección del tercero");
            RuleFor(r => r.Sexo).NotEmpty().WithMessage("Debe especificar el genero del tercero");
            RuleFor(r => r.Telefono).NotEmpty().WithMessage("Debe especificar el telefono del tercero");
            RuleFor(r => r.TipoIdentificacion).NotEmpty().WithMessage("Debe especificar el tipo de identificación del tercero");
            RuleFor(r => r.CorreoElectronico).NotEmpty().WithMessage("Debe especificar el correo electronico, es obligatorio para el tercero");
            RuleFor(r => TerceroExiste(r.Identificacion)).Equal(false).WithMessage("La identificación del tercero ya existe").When(t=> !string.IsNullOrEmpty(t.Identificacion));
            RuleFor(r => TipoIdentificacionEnumeration.IsValid(r.TipoIdentificacion)).Equal(true).WithMessage("El tipo de identifiación no es valido");
        }

        private bool TerceroExiste(string identificacion)
        {
            return _terceroRepository.Any(o => o.Identificacion == identificacion);
        }
    }
}
