using System;

namespace Domain.Contracts
{
    public interface INotificarPagoService
    {
        /// <summary>
        /// Notifica al sistema local de la aplicación de un pago
        /// </summary>
        /// <param name="pago">pago</param>
        public void Notificar(Pago pago);
    }

    public class NotificarPagoMockService : INotificarPagoService
    {
        public void Notificar(Pago pago)
        {
            Console.WriteLine($"Se aplico el pago del {pago.ReciboPago.GetTipo()} {pago.ReciboPago.Numero} por valor de ${pago.ReciboPago.Total} por {pago.MedioPago}");
        }
    }
}
