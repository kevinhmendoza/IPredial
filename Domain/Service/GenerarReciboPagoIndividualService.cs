using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service
{
    /// <summary>
    /// Para este caso este servicio se encargara de tomar la información con base al estado de cuenta 
    /// y generar el recibo de pago teniendo como ejemplo solo el primer registro del estado de cuenta  
    /// </summary>
    public class GenerarReciboPagoIndividualService : IReciboPagoFactoryService
    {
        public ReciboPago GenerarReciboPago(Tercero tercero, List<ConsultarEstadoCuentaSistemaLocalServiceModelView> estadoCuenta)
        {
            //Aqui se simula que el usuario da click sobre una vigencia del estado de cuenta
            var vigenciaSeleccionada = estadoCuenta.FirstOrDefault();
            return new ReciboPagoIndividual()
            {
                AreaTerreno = vigenciaSeleccionada.AreaTerreno.ToString(),
                Estado = "Pendiente",
                AvaluoPredio = vigenciaSeleccionada.Avaluo,
                DireccionPredio = vigenciaSeleccionada.Direccion,
                DireccionPropietario = tercero.Direccion,
                FechaLimitePago = DateTime.Now,
                FechaPago = null,
                Identificacion = vigenciaSeleccionada.IdentifiacionPropietario,
                NombreCompleto = tercero.NombreCompleto,
                Numero = $"20200000999",
                ReferenciaCatastral = vigenciaSeleccionada.ReferenciaCatastral,
                Pago = null,
                TipoPredio = vigenciaSeleccionada.Clase,
                TotalCapital = vigenciaSeleccionada.ValorCapital,
                TotalInteres = vigenciaSeleccionada.ValorInteres,
                VigenciaPagar = vigenciaSeleccionada.Vigencia
            };
        }
    }
}
