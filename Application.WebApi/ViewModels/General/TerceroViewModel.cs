using Core.Entities.Enumerations.General;

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
        public string CorreoElectronico { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCompleto { get; set; }
        private string CalcularNombreCompleto()
        {
            if (TipoIdentificacionEnumeration.IsPersonaJuridica(TipoIdentificacion))
            {
                return $"{Nombres} {Apellidos}".ToUpper().Trim();
            }
            return RazonSocial?.ToUpper().Trim() ?? "";
        }

        public void AsignarValoresCalculados()
        {
            if (!string.IsNullOrEmpty(Nombres)) { Nombres = Nombres.ToUpper().Trim(); } else { Nombres = null; }
            if (!string.IsNullOrEmpty(Apellidos)) { Apellidos = Apellidos.ToUpper().Trim(); } else { Apellidos = null; }
            if (!string.IsNullOrEmpty(RazonSocial)) { RazonSocial = RazonSocial.ToUpper().Trim(); } else { RazonSocial = null; }
            NombreCompleto = CalcularNombreCompleto();
        }
    }
}