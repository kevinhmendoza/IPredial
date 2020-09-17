using Headspring;
using System.Linq;

namespace Core.Entities.Enumerations.General
{
    public class ModulesEnumeration : Enumeration<ModulesEnumeration, string>
    {
        public static readonly ModulesEnumeration Seguridad = new ModulesEnumeration("Seguridad", "Seguridad");
        public static readonly ModulesEnumeration Configuracion = new ModulesEnumeration("Configuracion", "Configuración");

        private ModulesEnumeration(string value, string displayName) : base(value, displayName) {

        }

        public static bool IsValid(string Value)
        {
            return GetAll().Any(rr => rr.Value == Value);
        }
    }
}
