using Infrastructure.Security.Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text;

namespace Infrastructure.Security.Interactors
{
    public class ForzarConfirmEmailUserInteractor
    {
        private readonly ForzarConfirmEmailUserResponse _response;
        private readonly ApplicationUserManager _manager;
        public ForzarConfirmEmailUserInteractor()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_context);
            _response = new ForzarConfirmEmailUserResponse();
            _manager = new ApplicationUserManager(_userStore);
        }

        public ForzarConfirmEmailUserResponse ConfirmarUsuario(string usuario)
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
                if (!_usuario.EmailConfirmed)
                {

                    _usuario.EmailConfirmed = true;
                    IdentityResult result = _manager.Update(_usuario);

                    if (result.Succeeded)
                    {
                        _response.Mensaje = "¡Se ha activado la cuenta satisfactoriamente!";
                    }else
                    {
                        StringBuilder bld = new StringBuilder();
                        foreach (string error in result.Errors)
                        {
                            bld.Append("\n" + error);
                        }
                        _response.EstablecerError(bld.ToString());
                    }
                }
                else
                {
                    _response.EstablecerError("¡La cuenta ya ha sido activada!");
                }
            }
            return _response;
        }
    }

    public class ForzarConfirmEmailUserResponse
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
