using Domain.Builders;
using Domain.Contracts;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tercero = PatronBuilderCrearTercero();

            var recibo1 = PatronFactoryGenerarRecibo(tercero, "Direccion", "C 23 15 41");
            System.Console.ForegroundColor = System.ConsoleColor.White;
            if (recibo1 != null) { recibo1.Pagar(new NotificarPagoMockService(), DateTime.Now, "PSE"); }

            var recibo2 = PatronFactoryGenerarRecibo(tercero, "Identificacion", "1065654796");
            System.Console.ForegroundColor = System.ConsoleColor.White;
            if (recibo2 != null) { recibo2.Pagar(new NotificarPagoMockService(), DateTime.Now, "TARJETA DE CREDITO"); }

            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine($"FIN PROCESO");
            System.Console.ReadLine();
        }

        private static ReciboPago PatronFactoryGenerarRecibo(Tercero tercero, string tipo, string filtro)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Yellow;
            System.Console.WriteLine($"PATRON FACTORY");
            var estadoCuenta = PatronSingletonConsultarEstadoCuenta(tipo, filtro);
            System.Console.ForegroundColor = System.ConsoleColor.Yellow;
            if (!estadoCuenta.Any())
            {
                System.Console.WriteLine($"NO SE ENCONTRO INFORMACIÓN");
                System.Console.ReadLine();
                return null;
            }

            ReciboPago reciboPago = new FactoryRecibo().GetFactory(tercero, estadoCuenta);

            System.Console.WriteLine($"SE GENERO EL RECIBO DE PAGO!!!");
            System.Console.WriteLine($"Numero {reciboPago.Numero}");
            System.Console.WriteLine($"Tipo {reciboPago.GetTipo()}");
            System.Console.WriteLine($"Identificacion {reciboPago.Identificacion}");
            System.Console.WriteLine($"NombreCompleto {reciboPago.NombreCompleto}");
            System.Console.WriteLine($"Referencia Catastral {reciboPago.ReferenciaCatastral}");
            System.Console.WriteLine($"Direccion Predio {reciboPago.DireccionPredio}");
            System.Console.WriteLine($"Direccion Propietario {reciboPago.DireccionPropietario}");
            System.Console.WriteLine($"Avaluo Predio {reciboPago.AvaluoPredio}");
            System.Console.WriteLine($"Tipo Predio {reciboPago.TipoPredio}");
            System.Console.WriteLine($"Area Terreno {reciboPago.AreaTerreno}");
            System.Console.WriteLine($"Total Capital {reciboPago.TotalCapital}");
            System.Console.WriteLine($"Total Interes {reciboPago.TotalInteres}");
            System.Console.WriteLine($"Estado {reciboPago.Estado}");
            System.Console.WriteLine($"Fecha Pago {reciboPago.FechaPago}");
            System.Console.WriteLine($"Fecha LimitePago {reciboPago.FechaLimitePago}");
            System.Console.WriteLine($"Total {reciboPago.Total}");
            System.Console.WriteLine($"Periodos {reciboPago.Periodos}");

            System.Console.ReadLine();
            return reciboPago;
        }

        private static List<EstadoCuenta> PatronSingletonConsultarEstadoCuenta(string tipo, string filtro)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            System.Console.WriteLine($"PATRON SINGLETON");
            System.Console.WriteLine($"-----------------FILTRO POR {tipo.ToUpper()}: {filtro}--------------------");
            IConsultarEstadoCuentaSistemaLocalService service = ConsultarEstadoCuentaSingleton.GetInstance();
            var estadoCuenta = service.ConsultarEstadoCuenta(new ConsultarEstadoCuentaSistemaLocalServiceRequest
            {
                Filtro = filtro,
                TipoFiltro = tipo
            });
            foreach (var resultado in estadoCuenta)
            {
                System.Console.WriteLine($"Referencia Catastral {resultado.ReferenciaCatastral}");
                System.Console.WriteLine($"Identifiacion Propietario {resultado.IdentifiacionPropietario}");
                System.Console.WriteLine($"Propietario {resultado.Propietario}");
                System.Console.WriteLine($"Direccion {resultado.Direccion}");
                System.Console.WriteLine($"Avaluo {resultado.Avaluo}");
                System.Console.WriteLine($"Area Terreno {resultado.AreaTerreno}");
                System.Console.WriteLine($"Area Construida {resultado.AreaConstruida}");
                System.Console.WriteLine($"Clase {resultado.Clase}");
                System.Console.WriteLine($"Estrato {resultado.Estrato}");
                System.Console.WriteLine($"Destino Economico {resultado.DestinoEconomico}");
                System.Console.WriteLine($"Uso Suelo {resultado.UsoSuelo}");
                System.Console.WriteLine($"Numero Liquidacion {resultado.NumeroLiquidacion}");
                System.Console.WriteLine($"Vigencia {resultado.Vigencia}");
                System.Console.WriteLine($"Periodo {resultado.Periodo}");
                System.Console.WriteLine($"Valor Capital {resultado.ValorCapital}");
                System.Console.WriteLine($"Valor Interes {resultado.ValorInteres}");
                System.Console.WriteLine($"Total {resultado.Total}");
                System.Console.WriteLine("*********************************************");
            }
            System.Console.WriteLine("-----------------PULSE UNA TECLA PARA CONTINUAR--------------------");
            System.Console.ReadLine();
            return estadoCuenta;
        }

        private static Tercero PatronBuilderCrearTercero()
        {
            System.Console.ForegroundColor = System.ConsoleColor.Magenta;
            System.Console.WriteLine($"PATRON BUILDER");
            ITerceroBuilder terceroBuilder = new TerceroBuilder();
            System.Console.Write("Digite el tipo de identificación (NIT o CC): ");
            var tipoIdentificacion = System.Console.ReadLine();
            terceroBuilder.SetTipoIdentificacion(tipoIdentificacion);
            System.Console.Write("Digite la identificación: ");
            terceroBuilder.SetIdentificacion(System.Console.ReadLine());
            if (tipoIdentificacion.ToUpper().Trim() == "CC".ToUpper())
            {
                System.Console.Write("Digite sus nombres: ");
                terceroBuilder.SetNombres(System.Console.ReadLine());
                System.Console.Write("Digite sus apellidos: ");
                terceroBuilder.SetApellidos(System.Console.ReadLine());
                System.Console.Write("Digite su sexo (M o F): ");
                terceroBuilder.SetSexo(System.Console.ReadLine());
            }
            else
            {
                System.Console.Write("Digite su razón social: ");
                terceroBuilder.SetRazonSocial(System.Console.ReadLine());
            }
            System.Console.Write("Digite su telefono: ");
            terceroBuilder.SetTelefono(System.Console.ReadLine());
            System.Console.Write("Digite su direccion: ");
            terceroBuilder.SetDireccion(System.Console.ReadLine());
            System.Console.Write("Digite su correo electrónico: ");
            terceroBuilder.SetCorreoElectronico(System.Console.ReadLine());
            var tercero = terceroBuilder.Build();
            System.Console.WriteLine("-------------------------------------");
            System.Console.WriteLine(tercero.ToString());
            System.Console.WriteLine("-----------------PULSE UNA TECLA PARA CONTINUAR--------------------");
            System.Console.ReadLine();

            return tercero;
        }
    }

 
}
