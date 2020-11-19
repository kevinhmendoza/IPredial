using Domain.Contracts;
using System.Collections.Generic;

namespace Domain.Service
{
    public class FactoryRecibo
    {
        public IReciboPagoFactoryService GetFactory(List<EstadoCuenta> estadoCuenta)
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
            return reciboPagoFactoryService;
        }
    }
}
