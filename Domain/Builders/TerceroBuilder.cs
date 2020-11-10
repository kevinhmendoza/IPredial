using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders
{
    public class TerceroBuilder
    {
        private string TipoIdentificacion;
        private string Identificacion;
        private string Nombres;
        private string Apellidos;
        private string Sexo;
        private string Telefono;
        private string Direccion;
        private string CorreoElectronico;
        private string RazonSocial;

        public TerceroBuilder()
        {
        }

        #region Setterss
        public TerceroBuilder SetTipoIdentificacion(string TipoIdentificacion)
        {
            this.TipoIdentificacion = TipoIdentificacion;
            return this;
        }
        public TerceroBuilder SetIdentificacion(string Identificacion)
        {
            this.Identificacion = Identificacion;
            return this;
        }
        public TerceroBuilder SetNombres(string Nombres)
        {
            this.Nombres = Nombres;
            return this;
        }
        public TerceroBuilder SetApellidos(string Apellidos)
        {
            this.Apellidos = Apellidos;
            return this;
        }
        public TerceroBuilder SetSexo(string Sexo)
        {
            this.Sexo = Sexo;
            return this;
        }
        public TerceroBuilder SetTelefono(string Telefono)
        {
            this.Telefono = Telefono;
            return this;
        }
        public TerceroBuilder SetDireccion(string Direccion)
        {
            this.Direccion = Direccion;
            return this;
        }
        public TerceroBuilder SetCorreoElectronico(string CorreoElectronico)
        {
            this.CorreoElectronico = CorreoElectronico;
            return this;
        }
        public TerceroBuilder SetRazonSocial(string RazonSocial)
        {
            this.RazonSocial = RazonSocial;
            return this;
        }
        #endregion

        /// <summary>
        /// Construye un nuevo Tercero con la invormación suministrada en el builder
        /// </summary>
        /// <returns>Tercero</returns>
        public Tercero Build()
        {
            Tercero tercero = new Tercero();
            tercero.TipoIdentificacion = TipoIdentificacion;
            tercero.Identificacion = Identificacion;
            tercero.Nombres = Nombres;
            tercero.Apellidos = Apellidos;
            tercero.Sexo = Sexo;
            tercero.Telefono = Telefono;
            tercero.Direccion = Direccion;
            tercero.CorreoElectronico = CorreoElectronico;
            tercero.RazonSocial = RazonSocial;
            tercero.AsignarValoresCalculados();
            return tercero;
        }
    }
}
