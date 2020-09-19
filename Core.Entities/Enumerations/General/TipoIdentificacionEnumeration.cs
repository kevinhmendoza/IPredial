using Headspring;
using System.Linq;

namespace Core.Entities.Enumerations.General
{
    public class TipoIdentificacionEnumeration : Enumeration<TipoIdentificacionEnumeration, string>
    {
        public static readonly TipoIdentificacionEnumeration Cedula = new TipoIdentificacionEnumeration("Cedula", "Cédula de ciudadania", TipoPersonaEnumeration.Natural.Value);
        public static readonly TipoIdentificacionEnumeration Nit = new TipoIdentificacionEnumeration("Nit", "Número de identificación tributaria", TipoPersonaEnumeration.Juridica.Value);

        public string TipoPersona;
        public string TipoPersonaDisplayValue => TipoPersonaEnumeration.GetDisplayValue(TipoPersona);
        private TipoIdentificacionEnumeration(string value, string displayName, string tipoPersona) : base(value, displayName)
        {
            TipoPersona = tipoPersona;
        }


        public static bool IsPersonaJuridica(string Value)
        {
            var enumeration = GetAll().FirstOrDefault(rr => rr.Value == Value);
            return (enumeration?.TipoPersona ?? "") == TipoPersonaEnumeration.Juridica.Value;
        }

        public static bool IsValid(string Value)
        {
            return GetAll().Any(rr => rr.Value.ToUpper().Contains(Value.ToUpper()));
        }
    }
}

