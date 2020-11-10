using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IConsultarEstadoCuentaSistemaLocalService
    {
        ConsultarEstadoCuentaSistemaLocalServiceResponse ConsultarEstadoCuenta(ConsultarEstadoCuentaSistemaLocalServiceRequest request);
    }

    public class ConsultarEstadoCuentaSistemaLocalServiceRequest
    {
        public string TipoFiltro { get; set; }
        public string Filtro { get; set; }

    }

    public class ConsultarEstadoCuentaSistemaLocalServiceResponse
    {
        public List<ConsultarEstadoCuentaSistemaLocalServiceModelView> EstadoCuenta { get; set; }
        public ConsultarEstadoCuentaSistemaLocalServiceResponse(List<ConsultarEstadoCuentaSistemaLocalServiceModelView> estadoCuenta)
        {
            EstadoCuenta = estadoCuenta;
        }
    }

    public class ConsultarEstadoCuentaSistemaLocalServiceModelView
    {
        public string ReferenciaCatastral { get; set; }
        public string IdentifiacionPropietario { get; set; }
        public string Propietario { get; set; }
        public string Direccion { get; set; }
        public double Avaluo { get; set; }
        public decimal AreaTerreno { get; set; }
        public decimal AreaConstruida { get; set; }
        public string Clase { get; set; }
        public int Estrato { get; set; }
        public string DestinoEconomico { get; set; }
        public string UsoSuelo { get; set; }
        public string NumeroLiquidacion { get; set; }
        public int Vigencia { get; set; }
        public int Periodo { get; set; }
        public double ValorCapital { get; set; }
        public double ValorInteres { get; set; }
        public double Total => ValorCapital + ValorInteres;
    }
}
