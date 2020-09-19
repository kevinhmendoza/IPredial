using Application.WebApi.ViewModels.General;
using Infrastructure.Initialization;
using System.Collections.Generic;

namespace Application.WebApi.ViewModels.Security.GestionUsuarios
{
    public class ModificarTerceroWithUserRequest: TerceroViewModel
    {
        public int TerceroId => Id;
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