using Domain.Contracts;
using System;

namespace Domain
{
    public abstract class ReciboPago
    {
        public string Numero { get; set; }
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string ReferenciaCatastral  { get; set; }
        public string DireccionPredio { get; set; }
        public string DireccionPropietario { get; set; }
        public double AvaluoPredio { get; set; }
        public string TipoPredio { get; set; }
        public string AreaTerreno { get; set; }
        public double TotalCapital { get; set; }
        public double TotalInteres { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime FechaLimitePago { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Pago? Pago { get; set; }

        public double Total => TotalCapital + TotalInteres;
        /// <summary>
        /// Son los periodos que el contribuyente esta pagando, puede ser una vigencia o varias dependiendo del tipo del recibo
        /// </summary>
        public string Periodos => GetPeriodos();

        /// <summary>
        /// Obtiene los periodos o vigencias que el contribuyente desea pagar
        /// <para>Para recibo pago individual: es una sola vigencia ejemplo 2020</para>
        /// <para>Para recibo pago multiple: dos o mas vigencias ejemplo 2017,2018,2019,2020</para>
        /// </summary>
        /// <returns></returns>
        protected abstract string GetPeriodos();

        /// <summary>
        /// Retorna el tipo de recibo de pago
        /// </summary>
        /// <returns>Tipo de recibo de pago</returns>
        public abstract string GetTipo();

        /// <summary>
        /// Aplica el pago dependiendo el tipo del recibo
        /// </summary>
        /// <returns>Pago</returns>
        public Pago Pagar(INotificarPagoService notificarPago,DateTime fechaPago,string medioPago)
        {
            Pago pago = new Pago(this, fechaPago, medioPago);
            notificarPago.Notificar(pago);
            return pago;
        }
    }
}
