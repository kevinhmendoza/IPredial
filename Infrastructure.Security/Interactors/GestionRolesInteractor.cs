using Infrastructure.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Initialization.Interactors
{
    public class GestionRolesInteractor
    {
        private readonly GestionRolesResponse _response;
        private readonly ApplicationUserManager _manager;

        public GestionRolesInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            
            _manager = new ApplicationUserManager(_userStore);

            _response = new GestionRolesResponse();
        }

        public GestionRolesResponse GestionRoles(GestionRolesRequest request)
        {
            if (!EsValidoRequest(request)) { return _response; }

            int contador = 0;
            var user = _manager.FindByName(request.UserName);
            IdentityResult IdUserResult;
            if (user==null)
            {
                _response.EstablecerError($"El usuario {request.UserName} no esta registrado en nuestro sistema!!");
                return _response;
            }
            foreach (var item in request.Permisos)
            {
                bool hasRoolAnt = _manager.IsInRole(user.Id, item.Rol);
                if (item.hasRol != hasRoolAnt)//Si cambió
                {
                    try
                    {
                        if (item.hasRol)
                        {
                            IdUserResult = _manager.AddToRole(user.Id, item.Rol);
                        }
                        else
                        {
                            IdUserResult = _manager.RemoveFromRole(user.Id, item.Rol);
                        }
                        if (IdUserResult.Succeeded)
                        {
                            contador++;
                        }
                    }
                    catch (Exception ex)
                    {
                        _response.EstablecerError($"{ex.Message}");
                    }
                }
            }
            if (contador > 0)
            {
                _response.Mensaje += $"Se realizo la operación satisfactoriamente";
            }
            else
            {
                _response.EstablecerError($"Operación Realizada Satisfactoriamente, Pero no existen cambios!!");
            }
            return _response;
        }

        private bool EsValidoRequest(GestionRolesRequest request)
        {
            if (request==null) { _response.EstablecerError($"No se envio request");return false ; }
            if (string.IsNullOrEmpty(request.UserName))
            {
                _response.EstablecerError($"El usuario es obligatorio!!");
            }
            if (!request.Permisos.Any()) {
                _response.EstablecerError($"No se enviaron los permisos");
            }
            return !_response.Error;
        }
    }

    public class GestionRolesRequest
    {
        public List<PermisoSecurityDto> Permisos { get; set; }
        public string UserName { get; set; }
    }

    public class GestionRolesResponse
    {
        public GestionRolesResponse()
        {
            Permisos = new List<PermisoSecurityDto>();
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public List<PermisoSecurityDto> Permisos { get; set; }
        public void EstablecerError(string mensaje)
        {
            Error = true;
            Mensaje += $"{mensaje}\n";
        }
    }

    public class PermisoSecurityDto
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
