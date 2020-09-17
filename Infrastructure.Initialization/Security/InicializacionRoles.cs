using Core.Entities.General;
using Infrastructure.Data;
using Infrastructure.Initialization;
using Infrastructure.System;
using Infrastructure.System.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class InicializacionRoles
    {
        private readonly RoleManager<IdentityRole> roleMgr;

        private readonly UserManager<IdentityUser> userMgr;
        public InicializacionRoles()
        {
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>();
            roleMgr = new RoleManager<IdentityRole>(roleStore);

            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            userMgr = new UserManager<IdentityUser>(userStore);
        }
        private static string _ADMIN { get { return "admin"; } }
        public void Seed(CleanArchitectureContext _cleanArchitectureContext, ApplicationDbContext _applicationDbContext, Logger _logger)
        {
            _logger.Info("**************************************Inicializando Roles**************************************");

            IdentityUser user = userMgr.FindByName(_ADMIN);
            List<Menu> lt = new SystemMenu().Menus;

            foreach (var item in lt)
            {
                item.SubMenu = item.SubMenu.ToList();
                foreach (var itemSubMenu in item.SubMenu)
                {
                    string rol = itemSubMenu.Rol;
                    bool RolExiste = roleMgr.RoleExists(rol);
                    if ((!RolExiste))
                    {
                        roleMgr.Create(new IdentityRole(rol));
                        userMgr.AddToRole(user.Id, rol);

                        _logger.Info($"Se agrego el rol {rol} y se adiciono al usuario {_ADMIN}");
                    }
                    else
                    {
                        bool adminTieneElRol = userMgr.IsInRole(user.Id, rol);
                        if (!adminTieneElRol)
                        {
                            userMgr.AddToRole(user.Id, rol);

                            _logger.Info($"Se adiciono al usuario {_ADMIN} el rol {rol}");
                        }
                    }
                }
            }
        }
    }
}
