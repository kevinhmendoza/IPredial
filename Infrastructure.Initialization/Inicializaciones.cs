using Infrastructure.Data;
using Infrastructure.Initialization.Data;
using Infrastructure.Security;
using Infrastructure.System.Logging;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Initialization
{
    public class Inicializaciones
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly CleanArchitectureContext _cleanArchitectureContext;
        private readonly Logger _logger;
        private readonly UserManager<ApplicationUser> UserMgr;
        private readonly RoleManager<IdentityRole> RoleMgr;
        private bool _inicializoUsuarios;
        public Inicializaciones()
        {
            _applicationDbContext = new ApplicationDbContext();
            _cleanArchitectureContext = new CleanArchitectureContext();
            _logger = new Logger();
            UserMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_applicationDbContext));
            RoleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_applicationDbContext));
        }
        public void Seeders()
        {
            _inicializoUsuarios = UserMgr.Users.Any();
            if (!_inicializoUsuarios)
            {
                InicializacionData();
            }
            InicializacionSecurity();
        }

        private void InicializacionSecurity()
        {
            _logger.Info("**************************************Inicializando Seguridad**************************************");
            if (!_inicializoUsuarios)
            {
                new InicializacionUsuarios().Seed(UserMgr, RoleMgr, _applicationDbContext, _cleanArchitectureContext, _logger);
                _applicationDbContext.SaveChanges();
            }
            new InicializacionRoles().Seed(_cleanArchitectureContext, _applicationDbContext, _logger);
            _applicationDbContext.SaveChanges();
        }

        private void InicializacionData()
        {
            _logger.Info("**************************************Inicializando Datos**************************************");
            new InicializacionTerceros().Seed(_cleanArchitectureContext, _logger);
            _cleanArchitectureContext.SaveChanges();
        }
    }
}
