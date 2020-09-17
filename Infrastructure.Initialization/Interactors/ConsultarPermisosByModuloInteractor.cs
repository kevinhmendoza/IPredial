using Infrastructure.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Initialization.Interactors
{
    public class ConsultarPermisosByModuloInteractor
    {
        private readonly ConsultarPermisosByModuloResponse _response;
        private readonly SystemMenu _systemMenu;
        private readonly ApplicationUserManager _manager;
        
        public ConsultarPermisosByModuloInteractor()
        {
            _response = new ConsultarPermisosByModuloResponse();
            _systemMenu = new SystemMenu();
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);

            _manager = new ApplicationUserManager(_userStore);
        }

        public ConsultarPermisosByModuloResponse ConsultarPermisosPorModulo(ConsultarPermisosByModuloRequest _request)
        {
            if (!EsValidoRequest(_request)) { return _response; }

            var user = _manager.FindByName(_request.UserName);
            
            if (user == null)
            {
                _response.EstablecerError($"El usuario {_request.UserName} no esta registrado en nuestro sistema!!");
                return _response;
            }

            List<Menu> _Permisos = new List<Menu>();
            _systemMenu.Menus.Where(t => t.MenuId == _request.MenuId).ToList().ForEach(t =>
            {
                _Permisos.AddRange(t.SubMenu);
            });
            if (!_Permisos.Any())
            {
                _response.EstablecerError($"No se encontraron permisos asociados al modulo con id [{_request}]");
            }
            else
            {
                _response.Permisos = _Permisos.AsQueryable().Select(t => new PermisoInitializationDto()
                {
                    Descripcion = t.Descripcion,
                    Icono = t.Icono,
                    MenuId = t.MenuId,
                    hasRol = _manager.IsInRole(user.Id, t.Rol),
                    Modulo = t.Modulo,
                    Rol = t.Rol,
                    Titulo = t.Titulo
                }).ToList();
            }

            return _response;
        }

        private bool EsValidoRequest(ConsultarPermisosByModuloRequest _request)
        {
            if (_request==null) { _response.EstablecerError($"No se ha enviado el request");return false; }
            if (string.IsNullOrEmpty(_request.MenuId)) { _response.EstablecerError($"Debe enviar el modulo"); }
            if (string.IsNullOrEmpty(_request.UserName)) { _response.EstablecerError($"Debe enviar el usuario"); }
            return !_response.Error;
        }
    }

    public class ConsultarPermisosByModuloRequest
    {
        public string UserName { get; set; }
        public string MenuId { get; set; }
    }

    public class ConsultarPermisosByModuloResponse
    {
        public ConsultarPermisosByModuloResponse()
        {
            Permisos = new List<PermisoInitializationDto>();
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public List<PermisoInitializationDto> Permisos { get; set; }
        public void EstablecerError(string mensaje)
        {
            Error = true;
            Mensaje += $"{mensaje}\n";
        }
    }

    public class PermisoInitializationDto
    {
        public string MenuId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool hasRol { get; set; }
        public string Icono { get; set; }
        public string Modulo { get; set; }
        public string Rol { get; set; }
    }
}
