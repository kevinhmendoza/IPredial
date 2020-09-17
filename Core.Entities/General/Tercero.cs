using Core.Entities.Base;
using System.Collections.Generic;

namespace Core.Entities.General
{
    public class Tercero : AuditableEntity<int>
    {
        public Tercero()
        {
        }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
    }
}
