using Infrastructure.Security.Interactors;
using System.Web.Http;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [RoutePrefix("api/Confirm/Email")]
    [Authorize(Roles = "SEGURIDADChangePasswordToUser")]
    public class ForzarConfirmEmailController : ApiController
    {
        [Route("Forze/{UserName}")]
        public ForzarConfirmEmailUserResponse GetForzeConfirmEmail(string UserName)
        {
            ForzarConfirmEmailUserInteractor _interactor = new ForzarConfirmEmailUserInteractor();
            return _interactor.ConfirmarUsuario(UserName);
        }
    }
}
