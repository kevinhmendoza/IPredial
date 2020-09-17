using Core.Entities.General;
using Core.UseCase.Contracts;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCase.Security.GestionTercero
{
    public class  ConsultarTercerosInteractor : IRequestHandler<ConsultarTercerosRequest,  ConsultarTercerosResponse>
    {
        private readonly ITerceroRepository _repository;
        private readonly IValidator<ConsultarTercerosRequest> _validator;
        private readonly IMapper _mapper;

        public  ConsultarTercerosInteractor(IValidator<ConsultarTercerosRequest> validator, ITerceroRepository repository, IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ConsultarTercerosResponse> Handle(ConsultarTercerosRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            
            if (!validationResult.IsValid) return Task.FromResult(new ConsultarTercerosResponse(validationResult));

            List<Tercero> Personas = _repository.GetAll().ToList();
            return Task.FromResult(new ConsultarTercerosResponse(validationResult, Personas, _mapper));
        }
    }

    public class  ConsultarTercerosRequest : IRequest< ConsultarTercerosResponse>
    {
        public string Identificacion { get; set; }
        public string NumeroContrato { get; set; }

    }

    public class  ConsultarTercerosResponse
    {
        public ValidationResult ValidationResult { get; }
        public List<Tercero> PersonasResponse { get; }

        public  ConsultarTercerosResponse(ValidationResult validationResult, List<Tercero> Personas, IMapper mapper)
        {
            PersonasResponse = Personas;
            ValidationResult = validationResult;
        }
        public ConsultarTercerosResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }

    public class PersonaResponse
    {
        public int Id { get; set; }
        public string TipoIdentidad { get; set; }
        public string Identificacion { get; set; }
        public string DigitoVerificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NombreContacto { get; set; }
        public string RazonSocial { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get { return TipoIdentidad == "NIT" ? RazonSocial : string.Join(" ", PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido); } }
        public string Estado { get; set; }
        public string Telefono { get; set; }
    }

    public class  ConsultarPersonasRequestValidator : AbstractValidator< ConsultarTercerosRequest>
    {
        public  ConsultarPersonasRequestValidator()
        {
        }
    }
}
