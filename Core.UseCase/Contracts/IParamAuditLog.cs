using System;

namespace Core.UseCase.Contracts
{
    public interface IParamAuditLog
    {
        DateTime? FechaInicial { get; set; }
        DateTime? FechaFinal { get; set; }
        DateTime? HoraInicial { get; set; }
        DateTime? HoraFinal { get; set; }
        string Tabla { get; set; }
        string Interactor { get; set; }
        string Modulo { get; set; }
        string Evento { get; set; }
        string Id { get; set; }
        string Usuario { get; set; }
    }
}
