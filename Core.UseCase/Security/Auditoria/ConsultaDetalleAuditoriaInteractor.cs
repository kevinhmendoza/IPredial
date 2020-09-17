using Core.Entities.Base;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Audit;
using MediatR;
using SirccELC.Core.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCase.Security.Auditoria
{
    public class ConsultaDetalleAuditoriaInteractor : IRequestHandler<ConsultaDetalleAuditoriaRequest, ConsultaDetalleAuditoriaResponse>
    {
        private readonly ITerceroRepository _tercero;
        private readonly IAuditLogRepository _auditoria;
        private readonly IValidator<ConsultaDetalleAuditoriaRequest> _validator;

        public ConsultaDetalleAuditoriaInteractor(
            IValidator<ConsultaDetalleAuditoriaRequest> validator, 
            ITerceroRepository tercero,
            IAuditLogRepository auditoria)
        {
            _tercero = tercero;
            _auditoria = auditoria;
            _validator = validator;
        }

        public Task<ConsultaDetalleAuditoriaResponse> Handle(ConsultaDetalleAuditoriaRequest request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid) return Task.FromResult(new ConsultaDetalleAuditoriaResponse(validationResult));
            List<AuditLog> resultados = _auditoria.FindBy(x => x.RecordID == request.RecordID && x.TableName == request.Tabla).ToList();
            List<DetalleAuditoriaResponse> _resultados = resultados.GroupBy(x => new { x.EventDateLocalTime.Date, x.UserID }).Select(x => new DetalleAuditoriaResponse()
            {
                NombreUsuario = _tercero.FindBy(y => y.Identificacion == x.Key.UserID).FirstOrDefault()?.NombreCompleto ?? x.Key.UserID,
                UserID = x.Key.UserID,
                Date = x.Key.Date,
                Detalles = x.Select(g => new DetallesAuditoria()
                {
                    EventDateLocalTime = g.EventDateLocalTime,
                    Action = g.Interactor,
                    ColumnName = g.ColumnName,
                    EventDateUTC = g.EventDateUTC,
                    EventType = g.EventType,
                    Module = g.Module,
                    NewValue = g.NewValue,
                    OriginalValue = g.OriginalValue
                }).ToList()
            }).ToList();

            return Task.FromResult(new ConsultaDetalleAuditoriaResponse(validationResult, _resultados));
        }
    }
    public class ConsultaDetalleAuditoriaRequest : IRequest<ConsultaDetalleAuditoriaResponse>
    {
        public string Tabla { get; set; }
        public string RecordID { get; set; }
    }
    public class ConsultaDetalleAuditoriaResponse
    {
        public ValidationResult ValidationResult { get; }
        public List<DetalleAuditoriaResponse> Encontrados { get; set; }
        public ConsultaDetalleAuditoriaResponse(ValidationResult validationResult,List<DetalleAuditoriaResponse> resultados = null)
        {
            ValidationResult = validationResult;
            Encontrados = resultados;
        }
    }
    public class DetallesAuditoria
    {
        public DateTime EventDateUTC { get; set; }
        public DateTime EventDateLocalTime { get; set; }
        public string EventType { get; set; }
        public string Evento
        {
            get
            {
                return EventTypeEnumeration.GetDisplayValue(EventType);
            }
        }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string Action { get; set; }
        public string Module { get; set; }
    }

    public class DetalleAuditoriaResponse
    {
        public string UserID { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime EventDateUTC { get; set; }
        public DateTime Date { get; set; }
        public List<DetallesAuditoria> Detalles { get; set; }
    }
    public class ConsultaDetalleAuditoriaValidator : AbstractValidator<ConsultaDetalleAuditoriaRequest>
    {

        public ConsultaDetalleAuditoriaValidator()
        {
            RuleFor(r => ValidarExisteAlMenosUnParametroAConsultar(r)).Equal(true).WithName("Parametros").WithMessage("Debe especificar al menos un parametro para la consulta");
        }
        public bool ValidarExisteAlMenosUnParametroAConsultar(ConsultaDetalleAuditoriaRequest parametro)
        {
            if (!string.IsNullOrEmpty(parametro.RecordID) &&
                !string.IsNullOrEmpty(parametro.Tabla))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
