using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IReciboPagoFactoryService
    {

        /// <summary>
        /// Con base al estado de cuenta el sistema genera el recibo de pago
        /// </summary>
        /// <param name="estadoCuenta">estado de cuenta</param>
        /// <param name="tercero">tercero quien genera el recibo de pago</param>
        /// <returns>Recibo de pago</returns>
        ReciboPago GenerarReciboPago(Tercero tercero, List<ConsultarEstadoCuentaSistemaLocalServiceModelView> estadoCuenta);
    }
}
