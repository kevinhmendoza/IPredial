using Infrastructure.Security.Interactors;
using System.Web.Http;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [RoutePrefix("api/Password")]
    [Authorize(Roles = "SEGURIDADChangePasswordToUser")]
    public class CambiarPasswordController : ApiController
    {
        [Route("Change")]
        public CambiarPasswordResponse PostPermission(CambiarPasswordRequest request)
        {
            CambiarPasswordInteractor _interactor = new CambiarPasswordInteractor();
            return _interactor.CambiarPassword(request);
        }
    }
}
