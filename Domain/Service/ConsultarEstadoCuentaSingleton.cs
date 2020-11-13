using Domain.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service
{
    /// <summary>
    /// Se toma la decisión de hacerlo singleton ya que cuando se tenga acceso a las credenciales para comunicarse con el web service del sistema local,
    /// no se tendra que consultar las credenciales cada ves que se solicite un estado de cuenta de un contribuyente
    /// </summary>
    public class ConsultarEstadoCuentaSingleton : IConsultarEstadoCuentaSistemaLocalService
    {
        private static ConsultarEstadoCuentaSingleton instance;
        private ConsultarEstadoCuentaSingleton() { }
        public static ConsultarEstadoCuentaSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new ConsultarEstadoCuentaSingleton();
            }
            return instance;
        }

        /// <summary>
        /// Estamos simulando el llamado a el web service del sistema local
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Lista de estado de cuenta</returns>
        public List<EstadoCuenta> ConsultarEstadoCuenta(ConsultarEstadoCuentaSistemaLocalServiceRequest request)
        {
            var listaEstadoCuenta = InicializarDatos();

            if (request.TipoFiltro == "Direccion")
            {
                listaEstadoCuenta = listaEstadoCuenta.Where(t => t.Direccion == request.Filtro).ToList();
            }
            if (request.TipoFiltro == "ReferenciaCatastral")
            {
                listaEstadoCuenta = listaEstadoCuenta.Where(t => t.ReferenciaCatastral == request.Filtro).ToList();
            }
            if (request.TipoFiltro == "Identificacion")
            {
                listaEstadoCuenta = listaEstadoCuenta.Where(t => t.IdentifiacionPropietario == request.Filtro).ToList();
            }

            return listaEstadoCuenta;
        }

        #region Inicialización del mock
        private List<EstadoCuenta> InicializarDatos()
        {

            var estadoCuenta = new List<EstadoCuenta>();
            estadoCuenta.Add(new EstadoCuenta
            {
                IdentifiacionPropietario = "1065654796",
                AreaConstruida = 225,
                AreaTerreno = 100,
                Avaluo = 10000000,
                Clase = "URBANO",
                DestinoEconomico = "HABITACIONAL",
                Direccion = "C 23 15 41",
                Estrato = 4,
                NumeroLiquidacion = "20200001",
                Periodo = 1,
                Propietario = "Kevin Mendoza",
                ReferenciaCatastral = "0001000100010001",
                UsoSuelo = "",
                ValorCapital = 150000,
                ValorInteres = 0,
                Vigencia = 2020,


            });
            estadoCuenta.Add(new EstadoCuenta
            {
                IdentifiacionPropietario = "1065654796",
                AreaConstruida = 230,
                AreaTerreno = 90,
                Avaluo = 20000000,
                Clase = "URBANO",
                DestinoEconomico = "COMERCIAL",
                Direccion = "C 23 15 42",
                Estrato = 5,
                NumeroLiquidacion = "20190002",
                Periodo = 1,
                Propietario = "Kevin Mendoza",
                ReferenciaCatastral = "0001000100010002",
                UsoSuelo = "",
                ValorCapital = 170000,
                ValorInteres = 20000,
                Vigencia = 2019,


            });
            estadoCuenta.Add(new EstadoCuenta
            {
                IdentifiacionPropietario = "1065654796",
                AreaConstruida = 230,
                AreaTerreno = 90,
                Avaluo = 20000000,
                Clase = "URBANO",
                DestinoEconomico = "EDUCATIVO",
                Direccion = "C 23 15 43",
                Estrato = 5,
                NumeroLiquidacion = "20180003",
                Periodo = 1,
                Propietario = "Kevin Mendoza",
                ReferenciaCatastral = "0001000100010003",
                UsoSuelo = "",
                ValorCapital = 300000,
                ValorInteres = 40000,
                Vigencia = 2018,


            });

            estadoCuenta.Add(new EstadoCuenta
            {
                IdentifiacionPropietario = "123456789",
                AreaConstruida = 225,
                AreaTerreno = 100,
                Avaluo = 10000000,
                Clase = "URBANO",
                DestinoEconomico = "HABITACIONAL",
                Direccion = "C 23 15 44",
                Estrato = 4,
                NumeroLiquidacion = "20200004",
                Periodo = 1,
                Propietario = "Jhon Snow",
                ReferenciaCatastral = "0001000100010004",
                UsoSuelo = "",
                ValorCapital = 150000,
                ValorInteres = 0,
                Vigencia = 2020,


            });
            estadoCuenta.Add(new EstadoCuenta
            {
                IdentifiacionPropietario = "123456789",
                AreaConstruida = 230,
                AreaTerreno = 90,
                Avaluo = 20000000,
                Clase = "URBANO",
                DestinoEconomico = "COMERCIAL",
                Direccion = "C 23 15 45",
                Estrato = 5,
                NumeroLiquidacion = "20190005",
                Periodo = 1,
                Propietario = "Jhon Snow",
                ReferenciaCatastral = "0001000100010005",
                UsoSuelo = "",
                ValorCapital = 170000,
                ValorInteres = 20000,
                Vigencia = 2019,


            });
            estadoCuenta.Add(new EstadoCuenta
            {
                IdentifiacionPropietario = "123456789",
                AreaConstruida = 230,
                AreaTerreno = 90,
                Avaluo = 20000000,
                Clase = "URBANO",
                DestinoEconomico = "EDUCATIVO",
                Direccion = "C 23 15 46",
                Estrato = 5,
                NumeroLiquidacion = "20180006",
                Periodo = 1,
                Propietario = "Jhon Snow",
                ReferenciaCatastral = "0001000100010006",
                UsoSuelo = "",
                ValorCapital = 300000,
                ValorInteres = 40000,
                Vigencia = 2018,


            });


            return estadoCuenta;
        }
        #endregion
    }
}
