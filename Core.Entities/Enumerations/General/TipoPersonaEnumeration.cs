using Headspring;
using System.Linq;

namespace Core.Entities.Enumerations.General
{
    public class TipoPersonaEnumeration : Enumeration<TipoPersonaEnumeration, string>
    {
        public static readonly TipoPersonaEnumeration Natural = new TipoPersonaEnumeration("Natural", "Natural");
        public static readonly TipoPersonaEnumeration Juridica = new TipoPersonaEnumeration("Juridica", "Juridica");
        private TipoPersonaEnumeration(string value, string displayName) : base(value, displayName)
        {

        }

        public static string GetDisplayValue(string Value)
        {
            return GetAll().FirstOrDefault(rr => rr.Value == Value)?.DisplayName ?? Value;
        }

        public static bool IsValid(string Value)
        {
            return GetAll().Any(rr => rr.Value.ToUpper().Contains(Value.ToUpper()));
        }
    }
}