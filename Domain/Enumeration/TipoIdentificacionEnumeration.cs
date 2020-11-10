using Headspring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enumeration
{
    public class TipoIdentificacionEnumeration : Enumeration<TipoIdentificacionEnumeration, string>
    {
        public static readonly TipoIdentificacionEnumeration Cedula = new TipoIdentificacionEnumeration("Cedula", "Cédula de ciudadania", "Natural");
        public static readonly TipoIdentificacionEnumeration Nit = new TipoIdentificacionEnumeration("Nit", "Número de identificación tributaria", "Juridica");

        public string TipoPersona;
        private TipoIdentificacionEnumeration(string value, string displayName, string tipoPersona) : base(value, displayName)
        {
            TipoPersona = tipoPersona;
        }


        public static bool IsPersonaJuridica(string Value)
        {
            var enumeration = GetAll().FirstOrDefault(rr => rr.Value == Value);
            return (enumeration?.TipoPersona ?? "") == "Juridica";
        }

        public static bool IsValid(string Value)
        {
            return GetAll().Any(rr => rr.Value.ToUpper().Contains(Value.ToUpper()));
        }
    }
}
