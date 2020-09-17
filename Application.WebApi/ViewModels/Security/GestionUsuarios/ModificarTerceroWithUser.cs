using Application.WebApi.ViewModels.General;
using Infrastructure.Initialization;
using System.Collections.Generic;

namespace Application.WebApi.ViewModels.Security.GestionUsuarios
{
    public class ModificarTerceroWithUserRequest
    {
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int TerceroId { get; set; }
        public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class ModificarTerceroWithUserResponse:ResponseGenericViewModel
    {
        
    }

    public class TerceroWithUser:ResponseGenericViewModel
    {
        public TerceroWithUser()
        {
            Tercero = new TerceroViewModel();
            Usuario = new UserBasicViewModel();
        }
        public TerceroViewModel Tercero { get; set; }
        public UserBasicViewModel Usuario { get; set; }
    }

    public class TerceroWithUserAndPermission : TerceroWithUser
    {
        public TerceroWithUserAndPermission()
        {
            Permisos = new List<Permisos>();
        }
        public List<Permisos> Permisos { get; set; }
        public Permisos PermisoInicial{ get; set; }
    }
}