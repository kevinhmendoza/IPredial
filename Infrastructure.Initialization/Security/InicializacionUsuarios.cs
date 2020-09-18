using Core.Entities.General;
using Infrastructure.Data;
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
    public class InicializacionUsuarios
    {
        private Logger _logger;
        public void Seed(UserManager<ApplicationUser>  userMgr, RoleManager<IdentityRole>  roleMgr, ApplicationDbContext _applicationDbContext, CleanArchitectureContext _cleanArchitectureContext, Logger logger)
        {
            _logger = logger;
            _logger.Info("**************************************Inicializando Usuarios**************************************");
            // Access the application context and create result variables.
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            //var roleStore = new RoleStore<IdentityRole>(_applicationDbContext);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            //var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.

            //if (!roleMgr.RoleExists("admin"))
            //{
            //    roleMgr.Create(new IdentityRole { Name = "admin" });
            //}

            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            //var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_applicationDbContext));

            Tercero _tercero = _cleanArchitectureContext.Terceros.FirstOrDefault(t => t.Identificacion == "admin");

            if(_tercero!=null)
            {
                var appUser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "cleanarchitecture@hotmail.com",
                    EmailConfirmed = true,
                    FechaDesactivacion = ByADateTime.Now.AddYears(1),
                    Estado = "AC",
                    NombreCompleto=$"{_tercero.Nombres} {_tercero.Apellidos}",
                    TerceroId=_tercero.Id,
                    Identificacion=_tercero.Identificacion
                };
                IdUserResult = userMgr.Create(appUser, "IPredial2020.");

                // If the new "Admin" user was successfully created, 
                // add the "Admin" user to the "Administrator" role. 
                if (IdUserResult.Succeeded)
                {
                    Logger("Se creo el usuario admin");
                    //IdUserResult = userMgr.AddToRole(appUser.Id, "admin");
                    //if (!IdUserResult.Succeeded)
                    //{
                    //    // Handle the error condition if there's a problem adding the user to the role.
                    //    Logger("No se agregaron Roles Admin");
                    //}
                }
                else
                {
                    // Handle the error condition if there's a problem creating the new user. 
                    Logger("No se creo el usuario admin");
                }
            }
            else
            {
                Logger($"No se encontro el tercero para el usuario admin");
            }
        }

        public void Logger(string mensajes) {
            _logger.Info("No se creo el usuario admin");
        }        
    }
}
