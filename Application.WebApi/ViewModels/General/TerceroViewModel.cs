using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.WebApi.ViewModels.General
{
    public class TerceroViewModel
    {
        public int Id { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string NombreCompleto =>$"{Nombres} {Apellidos}".Trim();
        public string CorreoElectronico { get; set; }
    }
}