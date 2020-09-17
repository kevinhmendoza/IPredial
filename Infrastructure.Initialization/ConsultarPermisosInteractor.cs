using Core.Entities.Contracts;
using Infrastructure.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Security.Entities;
using System;

namespace Infrastructure.Initialization
{
    public class ConsultarPermisosInteractor
    {
        private readonly ApplicationDbContext _context;
        private readonly ISistema _userCurrent;
        public readonly ConsultarPermisosResponse _response;
        public ConsultarPermisosInteractor()
        {
            _context = new ApplicationDbContext();
            _userCurrent = new Sistema();
            _response = new ConsultarPermisosResponse();
        }
        public ConsultarPermisosResponse GetRoles(string userName = null)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            if (userName == null) { userName = _userCurrent.UserName; }

            var user = userMgr.FindByName(userName);

            List<Modulos> _Modulos = new List<Modulos>();
            List<Menu> ListaMenus = new SystemMenu().Menus.OrderBy(t => t.MenuId).ToList();
            foreach (Menu menu in ListaMenus)
            {
                Modulos modulo = new Modulos(menu);

                List<Permisos> _permisos = menu.SubMenu.Where(t => t.Rol != null).OrderBy(t => t.MenuId)
                    .Select(t => new Permisos
                    {
                        hasRol = (userMgr.IsInRole(user.Id, t.Rol)),
                        Modulo = t.Modulo,
                        Rol = t.Rol,
                        Titulo = t.Titulo,
                        Url = t.Url,
                        Prioridad = t.Prioridad,
                        Habilitado = t.Habilitado,
                        Descripcion = t.Descripcion,
                        Icono = t.Icono,
                    }).Distinct().Where(t => t.hasRol).ToList();

                if (_permisos.Any() && !_Modulos.Any(t => t.Modulo == menu.Modulo))
                {
                    _Modulos.Add(modulo);
                    _Modulos.FirstOrDefault(t => t.Modulo == menu.Modulo).SubMenu.AddRange(_permisos);

                    IdentificarPermisoInicial(user.PermisoActivo, _permisos);
                }
            }
            _response.Modulos = _Modulos.OrderBy(X => X.Modulo).ToList();

            return _response;
        }

        private void IdentificarPermisoInicial(InitialPermissionUser permisoActivo, List<Permisos> _permisos)
        {
            if (permisoActivo != null)
            {
                foreach (Permisos permiso in _permisos)
                {
                    if (permiso.Rol == permisoActivo.Permission)
                    {
                        _response.PermisoInicial = permiso;
                    }
                }
            }
        }
    }

    public class ConsultarPermisosResponse
    {
        public List<Modulos> Modulos { get; set; }
        public Permisos PermisoInicial { get; set; }
    }
    public class Modulos
    {
        public Modulos(Menu menu)
        {
            Icono = menu.Icono;
            Descripcion = menu.Descripcion;
            Titulo = menu.Titulo;
            Modulo = menu.Modulo;
            SubMenu = new List<Permisos>();
        }

        public string Icono { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }
        public string Modulo { get; set; }
        public List<Permisos> SubMenu { get; set; }
    }
    public class Permisos
    {
        public bool hasRol { get; set; }
        public string Modulo { get; set; }
        public string Titulo { get; set; }
        public string Rol { get; set; }
        public string Url { get; set; }
        public int Prioridad { get; set; }
        public bool Habilitado { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
    }
}
