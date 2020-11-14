using Domain.Contracts;
using System.Collections.Generic;

namespace Domain.Service
{
    public class FactoryRecibo
    {
        public ReciboPago GetFactory(Tercero tercero, List<EstadoCuenta> estadoCuenta)
        {
            IReciboPagoFactoryService reciboPagoFactoryService;
            if (estadoCuenta.Count == 1)
            {
                reciboPagoFactoryService = new GenerarReciboPagoIndividualService();
            }
            else
            {
                reciboPagoFactoryService = new GenerarReciboPagoMultipleService();
            }
            var reciboPago = reciboPagoFactoryService.GenerarReciboPago(tercero, estadoCuenta);
            return reciboPago;
        }
    }
}
