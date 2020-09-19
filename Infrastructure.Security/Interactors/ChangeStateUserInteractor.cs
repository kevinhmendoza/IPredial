using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Services
{
    public class ChangeStateUserInteractor
    {
        private ChangeStateUserResponse _response;
        private readonly ApplicationUserManager _manager;
        public ChangeStateUserInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            _manager = new ApplicationUserManager(_userStore);
        }
        
        public ChangeStateUserResponse Inactive(ChangeStateUserRequest request)
        {
            _response = new ChangeStateUserResponse();
            if (request.TerceroId <= 0) { _response.EstablecerError($"el terceroId es obligatorio!!"); }
            if (string.IsNullOrEmpty(request.UserName)) { _response.EstablecerError($"el user name es obligatorio!!"); }
            if (_response.Error) { return _response; }

            ApplicationUser _usuario = _manager.Users.FirstOrDefault(x => x.TerceroId == request.TerceroId);
            if (_usuario == null)
            {
                _response.EstablecerError($"No se encontro el usuario registrado en el sistema");
            }
            else
            {
                _usuario.Estado = request.Estado;

                IdentityResult result = _manager.Update(_usuario);

                if (result.Succeeded)
                {
                    _response.Mensaje = $"¡Se cambio el estado al usuario correctamente!";
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

    public class ChangeStateUserRequest
    {
        public string UserName { get; set; }
        public int TerceroId { get; set; }
        public string Estado { get; set; }
    }

    public class ChangeStateUserResponse
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
