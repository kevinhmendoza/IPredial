using Domain.Builders;
using Domain.Contracts;
using Domain.Service;
using System;

namespace Domain.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"PATRON SINGLETON");
            PatronSingleton("Direccion", "C 23 15 41");
            PatronSingleton("Identificacion", "1065654796");
            System.Console.WriteLine($"PATRON BUILDER");
            PatronBuilder();
        }

        private static void PatronSingleton(string tipo, string filtro)
        {
            System.Console.WriteLine($"-----------------FILTRO POR {tipo.ToUpper()}: {filtro}--------------------");
            IConsultarEstadoCuentaSistemaLocalService service = ConsultarEstadoCuentaSistemaLocalSingletonService.GetInstance();
            var respuesta=service.ConsultarEstadoCuenta(new ConsultarEstadoCuentaSistemaLocalServiceRequest
            {
                Filtro = filtro,
                TipoFiltro = tipo
            });
            foreach (var resultado in respuesta.EstadoCuenta)
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
            System.Console.WriteLine("-----------------PULSE UNA TECLA PARA TERMINAR--------------------");
            System.Console.ReadLine();
        }

        private static void PatronBuilder()
        {
            TerceroBuilder terceroBuilder = new TerceroBuilder();
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
            System.Console.WriteLine("-----------------PULSE UNA TECLA PARA TERMINAR--------------------");
            System.Console.ReadLine();
        }
    }
}
