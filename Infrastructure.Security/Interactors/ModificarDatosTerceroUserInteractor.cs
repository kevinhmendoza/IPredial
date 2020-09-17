using Infrastructure.Security.Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Interactors
{
    public class ModificarDatosTerceroUserInteractor
    {
        private readonly ModificarDatosTerceroUserResponse _response;
        private readonly ApplicationUserManager _manager;
        public ModificarDatosTerceroUserInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            _response = new ModificarDatosTerceroUserResponse();
            _manager = new ApplicationUserManager(_userStore);
        }

        public ModificarDatosTerceroUserResponse ModificarUsuario(int terceroId, string nombreCompleto, string identificacion, string userName, string email)
        {
            if (terceroId <= 0) { _response.EstablecerError($"el terceroId es obligatorio!!"); }
            if (string.IsNullOrEmpty(nombreCompleto)) { _response.EstablecerError($"el nombre del tercero es obligatorio!!"); }
            if (string.IsNullOrEmpty(identificacion)) { _response.EstablecerError($"la identificacion del tercero es obligatorio!!"); }
            if (string.IsNullOrEmpty(email)) { _response.EstablecerError($"el usuario es obligatorio!!"); }
            if (string.IsNullOrEmpty(userName)) { _response.EstablecerError($"el correo electronico es obligatorio!!"); }

            if (_response.Error) { return _response; }

            ApplicationUser _usuario = _manager.Users.FirstOrDefault(x => x.TerceroId == terceroId);
            if (_usuario == null)
            {
                _response.EstablecerError($"No se encontrao el usuario registrado en el sistema");
            }
            else
            {
                _usuario.EmailConfirmed = true;
                _usuario.Identificacion = identificacion;
                _usuario.NombreCompleto = nombreCompleto;
                _usuario.UserName = userName;
                _usuario.Email = email;

                IdentityResult result = _manager.Update(_usuario);

                if (result.Succeeded)
                {
                    _response.Mensaje = "¡Se ha Modificado el usuario correctamente!";
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

    public class ModificarDatosTerceroUserResponse
    {
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public void EstablecerError(string mensaje)
        {
            Error = true;
            Mensaje += $"{mensaje}\n";
        }
    }
}
