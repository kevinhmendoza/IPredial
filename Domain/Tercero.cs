using Domain.Enumeration;

namespace Domain
{
    public class Tercero
    {
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }
        public string RazonSocial { get; set; }
        public string NombreCompleto { get; private set; }

        public string TipoPersona { get; private set; }

        private string CalcularNombreCompleto()
        {
            if (TipoPersona == "Natural")
            {
                RazonSocial = null;
                return $"{Nombres} {Apellidos}".ToUpper().Trim();
            }
            Nombres = null;
            Apellidos = null;
            Sexo = null;
            return RazonSocial?.ToUpper().Trim() ?? "";
        }

        public void AsignarValoresCalculados()
        {
            if (!string.IsNullOrEmpty(Nombres)) { Nombres = Nombres.ToUpper().Trim(); } else { Nombres = null; }
            if (!string.IsNullOrEmpty(Apellidos)) { Apellidos = Apellidos.ToUpper().Trim(); } else { Apellidos = null; }
            if (!string.IsNullOrEmpty(RazonSocial)) { RazonSocial = RazonSocial.ToUpper().Trim(); } else { RazonSocial = null; }
            TipoPersona = TipoIdentificacionEnumeration.IsPersonaJuridica(TipoIdentificacion) ?
                "Juridica" :
                "Natural";
            NombreCompleto = CalcularNombreCompleto();
        }

        public override string ToString()
        {
            return $"T.I: {TipoIdentificacion} Identificación: {Identificacion},Nombre completo: {NombreCompleto}, Telefono: {Telefono}, Correo electronico: {CorreoElectronico} Direccion: {Direccion}";
        }
    }
}
