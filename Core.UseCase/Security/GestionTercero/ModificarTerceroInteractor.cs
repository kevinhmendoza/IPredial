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
using Core.UseCase.Dtos.General;

namespace Core.UseCase.Security.GestionTercero
{
    public class ModificarTerceroInteractor : IRequestHandler<ModificarTerceroRequest, ModificarTerceroResponse>, IInteractor
    {
        public string Module => ModulesEnumeration.Seguridad.Value;
        public string Name => GetType().Name;

        private readonly IModificarTerceroRequestValidator<ModificarTerceroRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public ModificarTerceroInteractor(IModificarTerceroRequestValidator<ModificarTerceroRequest> validator, IMapper mapper, IUnitOfWork iUnitOfWork)
        {
            _validator = validator;
            _mapper = mapper;
            _iUnitOfWork = iUnitOfWork;
        }

        public Task<ModificarTerceroResponse> Handle(ModificarTerceroRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new ModificarTerceroResponse(validationResult));

            Tercero TerceroModificar = _validator.GetTercero();

            _mapper.Map<ModificarTerceroRequest, Tercero>(request, TerceroModificar);

            TerceroModificar.AsignarValoresCalculados();

            _iUnitOfWork.Commit(this);

            TerceroDto TerceroDto = new TerceroDto();

            _mapper.Map<Tercero, TerceroDto>(TerceroModificar, TerceroDto);

            return Task.FromResult(new ModificarTerceroResponse(validationResult, TerceroDto));
        }
    }

    public class ModificarTerceroRequest : IRequest<ModificarTerceroResponse>
    {
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
    }

    public class ModificarTerceroResponse
    {
        public ValidationResult ValidationResult { get; }
        public string Mensaje { get; set; }
        public TerceroDto Tercero { get; }
        public ModificarTerceroResponse(ValidationResult validationResult, TerceroDto Personas)
        {
            Tercero = Personas;
            ValidationResult = validationResult;
        }
        public ModificarTerceroResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class ModificarTerceroRequestValidator : AbstractValidator<ModificarTerceroRequest>, IModificarTerceroRequestValidator<ModificarTerceroRequest>
    {
        private readonly ITerceroRepository _terceroRepository;
        private Tercero _tercero;
        public ModificarTerceroRequestValidator(ITerceroRepository terceroRepository)
        {
            _terceroRepository = terceroRepository;
            RuleFor(r => r.Identificacion).NotEmpty().WithMessage("Debe especificar el número de identificación del tercero");
            
            RuleFor(r => r.Direccion).NotEmpty().WithMessage("Debe especificar la dirección del tercero");
            RuleFor(r => r.Telefono).NotEmpty().WithMessage("Debe especificar el telefono del tercero");
            RuleFor(r => r.TipoIdentificacion).NotEmpty().WithMessage("Debe especificar el tipo de identificación del tercero");
            RuleFor(r => IdentificacionExiste(r.Identificacion, r.Id)).Equal(false).WithMessage("La identificación del tercero ya existe").When(t => !string.IsNullOrEmpty(t.Identificacion));
            RuleFor(r => TipoIdentificacionEnumeration.IsValid(r.TipoIdentificacion)).Equal(true).WithMessage("El tipo de identifiación no es valido");
            RuleFor(r => r.Nombres).NotEmpty().WithMessage("Debe especificar los nombres del tercero").When(t => !TipoIdentificacionEnumeration.IsPersonaJuridica(t.TipoIdentificacion));
            RuleFor(r => r.Apellidos).NotEmpty().WithMessage("Debe especificar los apellidos del tercero").When(t => !TipoIdentificacionEnumeration.IsPersonaJuridica(t.TipoIdentificacion));
            RuleFor(r => r.RazonSocial).NotEmpty().WithMessage("Debe especificar la razón social del tercero").When(t => TipoIdentificacionEnumeration.IsPersonaJuridica(t.TipoIdentificacion));
            RuleFor(r => r.Direccion).NotEmpty().WithMessage("Debe especificar la dirección del tercero");
            RuleFor(r => r.Sexo).NotEmpty().WithMessage("Debe especificar el genero del tercero").When(t => !TipoIdentificacionEnumeration.IsPersonaJuridica(t.TipoIdentificacion));
            RuleFor(t => TerceroExiste(t.Id)).NotNull().WithMessage("No se encontro el tercero").When(t => t.Id > 0);
        }

        public Tercero GetTercero()
        {
            return _tercero;
        }

        private Tercero TerceroExiste(int terceroId)
        {
            _tercero = _terceroRepository.Find(terceroId);
            return _tercero;
        }

        private bool IdentificacionExiste(string identificacion, int terceroId)
        {
            Tercero terceroValidation = _terceroRepository.FindFirstOrDefault(o => o.Identificacion == identificacion);
            if (terceroValidation != null && terceroValidation.Id != terceroId)
            {
                return true;
            }
            return false;
        }
    }

    public interface IModificarTerceroRequestValidator<in T>
    {
        ValidationResult Validate(T instance);
        Tercero GetTercero();
    }
}
