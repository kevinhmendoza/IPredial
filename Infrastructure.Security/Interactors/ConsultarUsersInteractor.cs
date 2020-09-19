using Infrastructure.Security.Dtos;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Security.Interactors
{
    public class ConsultarUsersInteractor
    {
        private readonly ConsultarUsersResponse _response;
        private readonly ApplicationUserManager _manager;
        public ConsultarUsersInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            _response = new ConsultarUsersResponse();
            _manager = new ApplicationUserManager(_userStore);
        }

        public ConsultarUsersResponse ConsultarUsuarios()
        {
            List<ApplicationUser> Usuarios = _manager.Users.ToList();
            if (!Usuarios.Any())
            {
                _response.EstablecerError($"No se encontraron usuarios registrados en el sistema");
            }
            else
            {
                _response.Usuarios = Usuarios.AsQueryable().Select(t => new ApplicationUserDto()
                {
                    Email = t.Email,
                    LockoutEndDateUtc = t.LockoutEndDateUtc,
                    EmailConfirmed = t.EmailConfirmed,
                    LockoutEnabled = t.LockoutEnabled,
                    AccessFailedCount = t.AccessFailedCount,
                    UserName = t.UserName,
                    FechaDesactivacion = t.FechaDesactivacion,
                    NombreCompleto = t.NombreCompleto,
                    Identificacion = t.Identificacion,
                    TerceroId = t.TerceroId,
                    Estado = t.Estado,
                }).OrderBy(t => t.TerceroId).ToList();
            }
            return _response;
        }
    }

    public class ConsultarUsersResponse
    {
        public ConsultarUsersResponse()
        {
            Usuarios = new List<ApplicationUserDto>();
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public List<ApplicationUserDto> Usuarios { get; set; }
        public void EstablecerError(string mensaje)
        {
            Error = true;
            Mensaje += $"{mensaje}\n";
        }
    }
}
