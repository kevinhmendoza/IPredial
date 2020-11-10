using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service
{
    /// <summary>
    /// Para este caso este servicio se encargara de tomar la información con base al estado de cuenta 
    /// y generar el recibo de pago teniendo como ejemplo que el usuario envia las vigencias seleccionadas
    /// </summary>
    public class GenerarReciboPagoMultipleService : IReciboPagoFactoryService
    {
        public ReciboPago GenerarReciboPago(Tercero tercero, List<ConsultarEstadoCuentaSistemaLocalServiceModelView> estadoCuenta)
        {
            //Aqui se simula que el usuario selecciono las vigencias que desea cancelar
            var estadoDefault = estadoCuenta.FirstOrDefault();
            return new ReciboPagoMultiple()
            {
                AreaTerreno = estadoDefault.AreaTerreno.ToString(),
                Estado = "Pendiente",
                AvaluoPredio = estadoDefault.Avaluo,
                DireccionPredio = estadoDefault.Direccion,
                DireccionPropietario = tercero.Direccion,
                FechaLimitePago = DateTime.Now,
                FechaPago = null,
                Identificacion = estadoDefault.IdentifiacionPropietario,
                NombreCompleto = tercero.NombreCompleto,
                Numero = $"20200000152",
                ReferenciaCatastral = estadoDefault.ReferenciaCatastral,
                Pago = null,
                TipoPredio = estadoDefault.Clase,
                TotalCapital = estadoDefault.ValorCapital,
                TotalInteres = estadoDefault.ValorInteres,
                VigenciasPagar = estadoCuenta.Select(t => t.Vigencia).ToList(),
            };
        }
    }
}
