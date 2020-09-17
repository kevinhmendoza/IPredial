using Headspring;
using System.Linq;

namespace Core.Entities.Enumerations.General
{
    public class TipoIdentificacionEnumeration : Enumeration<TipoIdentificacionEnumeration, string>
    {
        public static readonly TipoIdentificacionEnumeration Cedula = new TipoIdentificacionEnumeration("Cedula", "Cedula");
        public static readonly TipoIdentificacionEnumeration Nit = new TipoIdentificacionEnumeration("Nit", "Nit");
        public static readonly TipoIdentificacionEnumeration RegistroCivil = new TipoIdentificacionEnumeration("Registro Civil", "Registro Civil");
        public static readonly TipoIdentificacionEnumeration TargetaIdentidad = new TipoIdentificacionEnumeration("Targeta Identidad", "Targeta Identidad");

        private TipoIdentificacionEnumeration(string value, string displayName) : base(value, displayName) {
           
        }

        public static bool IsValid(string Value)
        {
            return GetAll().Any(rr => rr.Value == Value);
        }
    }
}
