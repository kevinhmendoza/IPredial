using System;

namespace Domain
{
    public class Pago
    {
        public Pago(ReciboPago recibo, DateTime fechaPago, string medioPago)
        {
            ReciboPago = recibo;
            FechaPago = fechaPago;
            MedioPago = medioPago;
        }
        public ReciboPago ReciboPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string MedioPago { get; set; }
    }
}
