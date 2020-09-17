using Headspring;
using System.Linq;

namespace Core.Entities.Enumerations.General
{
    public class AccionEnumeration : Enumeration<AccionEnumeration, string>
    {
        public static readonly AccionEnumeration Registrar = new AccionEnumeration("Registrar", "Registrar");
        public static readonly AccionEnumeration Actualizar = new AccionEnumeration("Actualizar", "Actualizar");
        public static readonly AccionEnumeration Eliminar = new AccionEnumeration("Eliminar", "Eliminar");

        private AccionEnumeration(string value, string displayName) : base(value, displayName) {

        }

        public static bool IsValid(string Value)
        {
            return GetAll().Any(rr => rr.Value == Value);
        }
    }
}
