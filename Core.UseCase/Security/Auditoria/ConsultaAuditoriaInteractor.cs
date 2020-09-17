using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Core.UseCase.Contracts;
using SirccELC.Core.UseCase;
using Infrastructure.Audit;

namespace Core.UseCase.Security.Auditoria
{
    public class ConsultaAuditoriaInteractor : IRequestHandler<ConsultaAuditoriaRequest, ConsultaAuditoriaResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITerceroRepository _tercero;
        private readonly IAuditLogRepository _auditoria;
        private readonly IValidator<ConsultaAuditoriaRequest> _validator;

        public ConsultaAuditoriaInteractor(
            IValidator<ConsultaAuditoriaRequest> validator, 
            IMapper mapper, 
            ITerceroRepository tercero,
            IAuditLogRepository auditoria)
        {
            _tercero = tercero;
            _mapper = mapper;
            _auditoria = auditoria;
            _validator = validator;
        }

        public Task<ConsultaAuditoriaResponse> Handle(ConsultaAuditoriaRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new ConsultaAuditoriaResponse(validationResult));
            List<AuditLog> resultados = _auditoria.FindBy(request).OrderByDescending(x => x.EventDateLocalTime).ToList();
            List<AuditoriaResponse> _resultados = new List<AuditoriaResponse>();
            _mapper.Map(resultados, _resultados);
            _resultados.ForEach(x => x.NombreUsuario = _tercero.FindBy(y => y.Identificacion == x.UserID).FirstOrDefault()?.NombreCompleto ?? x.UserID);

            return Task.FromResult(new ConsultaAuditoriaResponse(validationResult, _resultados));
        }
    }
    public class ConsultaAuditoriaRequest : IRequest<ConsultaAuditoriaResponse>, IParamAuditLog
    {
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime? HoraInicial { get; set; }
        public DateTime? HoraFinal { get; set; }
        public string Tabla { get; set; }
        public string Modulo { get; set; }
        public string Evento { get; set; }
        public string Usuario { get; set; }
        public string Id { get; set; }
        public string Interactor { get; set; }
    }
    public class ConsultaAuditoriaResponse
    {
        public ValidationResult ValidationResult { get; }
        public List<AuditoriaResponse> Encontrados { get; set; }
        public ConsultaAuditoriaResponse(ValidationResult validationResult,List<AuditoriaResponse> resultados = null)
        {
            ValidationResult = validationResult;
            Encontrados = resultados;
        }
    }
    public class AuditoriaResponse
    {
        public string UserID { get; set; }
        public DateTime EventDateUTC { get; set; }
        public DateTime EventDateLocalTime { get; set; }
        public string EventType { get; set; }
        public string TableName { get; set; }
        public string RecordID { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string Module { get; set; }
        public string Interactor { get; set; }
        public string IpAddress { get; set; }
        public string HostName { get; set; }
        //Nuevos Campos
        public string NombreUsuario { get; set; }
        public string Evento
        { get
            {
                return Entities.Base.EventTypeEnumeration.GetDisplayValue(EventType);
            }
        }
    }
    public class ConsultaAuditoriaValidator : AbstractValidator<ConsultaAuditoriaRequest>
    {

        public ConsultaAuditoriaValidator()
        {
            RuleFor(r => ValidarSiExisteAlMenosUnParametroAConsultar(r)).Equal(true).WithName("Parametros").WithMessage("Debe especificar al menos un parametro para la consulta");
            RuleFor(r => r.FechaFinal.HasValue && r.FechaInicial.HasValue).Equal(true).WithMessage("debe especificar la fecha inicial y la fecha final").When(t => t != null).When(x => x.FechaFinal.HasValue || x.FechaInicial.HasValue);
            RuleFor(r => r.HoraFinal.HasValue && r.HoraInicial.HasValue).Equal(true).WithMessage("debe especificar la hora inicial y la hora final").When(t => t != null).When(x => x.HoraFinal.HasValue || x.HoraInicial.HasValue);
        }

        public bool ValidarFecha(DateTime? fecha)
        {
            if (!fecha.HasValue) return false;
            return fecha.Value.Year > 1;
        }
        public bool ValidarHora(DateTime? fecha)
        {
            return fecha.HasValue;
        }
        public bool ValidarSiExisteAlMenosUnParametroAConsultar(ConsultaAuditoriaRequest parametro)
        {
            if (string.IsNullOrEmpty(parametro.Interactor) &&
                string.IsNullOrEmpty(parametro.Modulo) &&
                string.IsNullOrEmpty(parametro.Tabla) &&
                string.IsNullOrEmpty(parametro.Usuario) &&
                string.IsNullOrEmpty(parametro.Id) &&
                !ValidarFecha(parametro.FechaInicial) &&
                !ValidarFecha(parametro.FechaFinal) &&
                !ValidarHora(parametro.HoraInicial) &&
                !ValidarHora(parametro.HoraFinal))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
