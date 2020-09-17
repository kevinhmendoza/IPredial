using System;
using System.Collections.Generic;

namespace Application.WebApi.ViewModels.Security.Auditoria
{
    public class DatosAuditoriaViewsModel
    {
        public DatosAuditoriaViewsModel()
        {
            Modulos = new List<string>();
            Acciones = new List<string>();
            Tablas = new List<string>();
        }
        public List<string> Modulos { get; set; }
        public List<string> Acciones { get; set; }
        public List<string> Tablas { get; set; }
    }

    public class ConsultaAuditoriaResponseViewModel
    {
        public ConsultaAuditoriaResponseViewModel()
        {
            Auditorias = new List<AuditoriaViewModel>();
        }
        public List<AuditoriaViewModel> Auditorias { get; set; }
    }

    public class AuditoriaViewModel
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
        public string Evento { get; set; }
    }

    public class DetallesAuditoriaViewsModel
    {
        public DateTime EventDateUTC { get; set; }
        public DateTime EventDateLocalTime { get; set; }
        public string EventType { get; set; }
        public string Evento { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string Action { get; set; }
        public string Accion { get; set; }
        public string Module { get; set; }
        public string Modulo { get; set; }
    }

    public class DetalleAuditoriaResponseViewsModel
    {
        public string UserID { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime EventDateUTC { get; set; }
        public DateTime Date { get; set; }
        public List<DetallesAuditoriaViewsModel> Detalles { get; set; }
    }
}