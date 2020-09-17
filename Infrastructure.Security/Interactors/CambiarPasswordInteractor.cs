using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System;

namespace Infrastructure.Security.Interactors
{
    public class CambiarPasswordInteractor
    {
        private readonly CambiarPasswordResponse _response;
        private readonly ApplicationUserManager _manager;
        public CambiarPasswordInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            _response = new CambiarPasswordResponse();
            _manager = new ApplicationUserManager(_userStore);
        }

        public CambiarPasswordResponse CambiarPassword(CambiarPasswordRequest _request)
        {
            if (!EsValidoRequest(_request)) { return _response; }

            ApplicationUser _usuario = _manager.Users.FirstOrDefault(x => x.UserName == _request.UserName);
            if (_usuario == null)
            {
                _response.EstablecerError($"No se encontro el usuario registrado en el sistema");
            }
            else
            {
                try
                {
                    _manager.RemovePassword(_usuario.Id);
                    _manager.AddPassword(_usuario.Id, _request.Password);
                    _response.Mensaje = "Se realizo la operación satisfactoriamente.";
                }
                catch (Exception ex)
                {
                    _response.EstablecerError($"Error de App: { ex.Message}");
                }
            }
            return _response;
        }

        private bool EsValidoRequest(CambiarPasswordRequest _request)
        {
            if (_request == null) { _response.EstablecerError($"Se debe enviar el request!!"); return false; }
            if (string.IsNullOrEmpty(_request.UserName)) { _response.EstablecerError($"el usuario es obligatorio!!"); }
            if (string.IsNullOrEmpty(_request.Password))
            {
                _response.EstablecerError($"la nueva contraseña inicial es obligatoria!!");
            }
            else if (string.IsNullOrEmpty(_request.PasswordConfirm))
            {
                _response.EstablecerError($"la contraseña de confirmacion es obligatoria!!");
            }
            else if (_request.Password != _request.PasswordConfirm)
            {
                _response.EstablecerError($"La contraseña inicial y la de confirmación deben ser iguales");
            }
            return !_response.Error;
        }
    }

    public class CambiarPasswordRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class CambiarPasswordResponse
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
