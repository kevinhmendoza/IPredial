using Infrastructure.Security.Dtos;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace Infrastructure.Security.Interactors
{
    public class ConsultarUserInteractor
    {
        private readonly ConsultarUserResponse _response;
        private readonly ApplicationUserManager _manager;
        public ConsultarUserInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            _response = new ConsultarUserResponse();
            _manager = new ApplicationUserManager(_userStore);
        }

        public ConsultarUserResponse ConsultarUsuario(string usuario)
        {
            if (string.IsNullOrEmpty(usuario)) { _response.EstablecerError($"el usuario es obligatorio!!"); }

            if (_response.Error) { return _response; }

            ApplicationUser _usuario = _manager.Users.FirstOrDefault(x=>x.UserName==usuario);
            if (_usuario==null)
            {
                _response.EstablecerError($"No se encontrao el usuario registrado en el sistema");
            }
            else
            {
                _response.Usuario = new ApplicationUserDto()
                {
                    Email = _usuario.Email,
                    LockoutEndDateUtc = _usuario.LockoutEndDateUtc,
                    EmailConfirmed = _usuario.EmailConfirmed,
                    LockoutEnabled = _usuario.LockoutEnabled,
                    AccessFailedCount = _usuario.AccessFailedCount,
                    UserName = _usuario.UserName,
                    FechaDesactivacion = _usuario.FechaDesactivacion,
                    NombreCompleto = _usuario.NombreCompleto,
                    Identificacion = _usuario.Identificacion,
                    TerceroId = _usuario.TerceroId,
                    Estado = _usuario.Estado,
                }; 
            }
            return _response;
        }
    }

    public class ConsultarUserResponse
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
