using Headspring;
using System.Linq;

namespace Core.Entities.Enumerations.General
{
    public class StatesGeneralEnumeration : Enumeration<StatesGeneralEnumeration, string>
    {
        public static readonly StatesGeneralEnumeration Activo = new StatesGeneralEnumeration("Activo", "Activo");
        public static readonly StatesGeneralEnumeration Inactivo= new StatesGeneralEnumeration("Inactivo", "Inactivo");

        private StatesGeneralEnumeration(string value, string displayName) : base(value, displayName) {
           
        }

        public static bool IsValid(string Value)
        {
            return GetAll().Any(rr => rr.Value == Value);
        }
    }
}
