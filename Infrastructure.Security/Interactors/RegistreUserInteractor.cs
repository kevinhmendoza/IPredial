using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.IO;
using System.Net;

namespace Infrastructure.Security.Interactors
{
    public class RegistreUserInteractor
    {
        private readonly RegisterUserResponse _response;
        private readonly ApplicationUserManager _manager;
        private readonly ApplicationDbContext _context;
        private readonly UserStore<ApplicationUser> _userStore;

        public RegistreUserInteractor(ApplicationUserManager userManager)
        {
            _manager = userManager;
            _response = new RegisterUserResponse();
            _context = new ApplicationDbContext();
            _userStore = new UserStore<ApplicationUser>(_context);
        }

        public RegisterUserResponse CreateUser(Usuario usuario, string UrlConfirmar)
        {
            if (IsUrlValida(UrlConfirmar))
            {
                CrearUsuario(usuario);
                if (!_response.Error)
                {
                    EnviarCorreoDeConfirmacion(usuario, UrlConfirmar, "confirm");
                }
            }
            else
            {
                _response.EstablecerError($"La url {UrlConfirmar} no es valida");
            }
            return _response;
        }

        private void EnviarCorreoDeConfirmacion(Usuario User, string UrlConfirmar, string Tipo)
        {
            string code = _manager.GenerateEmailConfirmationToken(User.UserId);
            code = WebUtility.UrlEncode(code);
            UrlConfirmar = UrlConfirmar.Replace("user.Id", User.UserId);
            var callbackUrl = UrlConfirmar.Replace("codigo", code);
            this._manager.SendEmail(User.UserId, GetAsunto(Tipo), WebUtility.HtmlDecode(GetMensaje(User, callbackUrl, Tipo)));
            _response.Mensaje += $"Se ha enviado un Correo a {User.Email} para que Confirme su Cuenta de Usuario";
        }

        public string GetAsunto(string Tipo)
        {
            if (Tipo == "forgot")
            {
                return "Cambio de Contraseña";
            }
            else
            {
                return "Confirme su Cuenta";
            }
        }
        private static string GetMensaje(Usuario usuario, string callbackUrl, string tipo)
        {
            if (tipo == "forgot")
            {
                return "Para cambiar tu contraseña has click <a href=\"" + callbackUrl + "\">aquí</a>";
            }
            else
            {
                return BodyConfirm(usuario, callbackUrl);
            }
        }
        public static string BodyConfirm(Usuario usuario, string urlCallback)
        {
            string filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Templates\MailConfirm.html";
            string text = File.ReadAllText(filePath);
            text = text.Replace("{{Usuario.NombreCompleto}}", usuario.NombreCompleto);
            text = text.Replace("{{Usuario.UserName}}", usuario.UserName);
            text = text.Replace("{{Usuario.Password}}", usuario.Password);
            text = text.Replace("{{UrlConfirmarUsuario}}", urlCallback);
            return text;
        }

        public static bool IsUrlValida(string url)
        {
            Uri uriResult;
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            else
            {
                return (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uriResult)
                  && uriResult.Scheme == Uri.UriSchemeHttp);
            }

        }

        private void CrearUsuario(Usuario Reg)
        {
            ApplicationUser User = new ApplicationUser() { UserName = Reg.UserName, Email = Reg.Email, EmailConfirmed = false, FechaDesactivacion = Reg.FechaDesactivacion, Estado = "AC", Identificacion = Reg.Identificacion, NombreCompleto = Reg.NombreCompleto, TerceroId = Reg.TerceroId };
            IdentityResult result = _manager.Create(User, Reg.Password);
            if (result.Succeeded)
            {
                Reg.UserId = User.Id;
                _response.Error = false;
                _response.Mensaje = $"El usuario {User.UserName} fue creado satisfactoriamente!";
            }
            else
            {
                GetErrorResults(result);
            }
        }

        private void GetErrorResults(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                _response.EstablecerError(error);
            }
        }


    }

    public class RegisterUserResponse
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
