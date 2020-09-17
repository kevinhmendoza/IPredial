using Infrastructure.Security.Interactors;
using System.Web.Http;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [RoutePrefix("api/User")]
    [Authorize(Roles = "SEGURIDADGestionUsuarios")]
    public class GetUsersController : ApiController
    {
        [Route("Get/All")]
        public ConsultarUsersResponse Get()
        {
            ConsultarUsersInteractor _interactor = new ConsultarUsersInteractor();
            return _interactor.ConsultarUsuarios();
        }
    }
}
