using Core.Entities.Enumerations.General;
using Infrastructure.Security.Dtos;
using Infrastructure.Security.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Interactors
{
    public class AsignarPermisoInitialInteractor
    {
        private readonly AsignarPermisoInitialResponse _response;
        private readonly ApplicationUserManager _manager;
        public AsignarPermisoInitialInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            _response = new AsignarPermisoInitialResponse();
            _manager = new ApplicationUserManager(_userStore);
        }

        public AsignarPermisoInitialResponse AsignarPermisoInicial(string usuario, string nombrePermiso)
        {
            if (string.IsNullOrEmpty(usuario)) { _response.EstablecerError($"el usuario es obligatorio!!"); }

            if (string.IsNullOrEmpty(nombrePermiso)) { _response.EstablecerError($"el nombre del permiso es obligatorio!!"); }

            if (_response.Error) { return _response; }

            ApplicationUser _usuario = _manager.Users.FirstOrDefault(x => x.UserName == usuario);
            if (_usuario == null)
            {
                _response.EstablecerError($"No se encontrao el usuario registrado en el sistema");
            }
            else
            {
                _usuario.PermisosInitiales.ToList().ForEach(t => t.State = StatesGeneralEnumeration.Inactivo.Value);

                _usuario.PermisosInitiales.Add(new InitialPermissionUser()
                {
                    Permission=nombrePermiso,
                    State= StatesGeneralEnumeration.Activo.Value
                });

                IdentityResult result = _manager.Update(_usuario);

                if (result.Succeeded)
                {
                    _response.Mensaje = "¡Se ha activado la cuenta satisfactoriamente!";
                }
                else
                {
                    StringBuilder bld = new StringBuilder();
                    foreach (string error in result.Errors)
                    {
                        bld.Append("\n" + error);
                    }
                    _response.EstablecerError(bld.ToString());
                }
            }
            return _response;
        }
    }

    public class AsignarPermisoInitialResponse
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public ApplicationUserDto Usuario { get; set; }
        public void EstablecerError(string mensaje)
        {
            Error = true;
            Mensaje += $"{mensaje}\n";
        }
    }
}
