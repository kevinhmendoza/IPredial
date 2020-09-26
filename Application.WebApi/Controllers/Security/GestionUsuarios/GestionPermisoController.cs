using Infrastructure.Initialization.Interactors;
using Infrastructure.Security.Interactors;
using System.Web.Http;

namespace Application.WebApi.Controllers.Security.GestionUsuarios
{
    [RoutePrefix("api/Roles")]
    [Authorize(Roles = "Seguridad.PermisosToUser")]
    public class GestionPermisoController : ApiController
    {
        [Route("User/{userName}")]
        public IHttpActionResult Get(string userName)
        {
            ConsultarUserInteractor _interactor = new ConsultarUserInteractor();

            ConsultarUserResponse _rersponse = _interactor.ConsultarUsuario(userName);

            if (!_rersponse.Error)
            {
                return Ok(_rersponse);
            }

            return BadRequest(_rersponse.Mensaje);

        }

        [Route("Modules")]
        public ConsultarModulosResponse GetAllModules()
        {
            ConsultarModulosInteractor _interactor = new ConsultarModulosInteractor();
            return _interactor.ConsultarAllModules();
        }

        [Route("Get/Permission")]
        public ConsultarPermisosByModuloResponse PostPermission(ConsultarPermisosByModuloRequest request)
        {
            ConsultarPermisosByModuloInteractor _interactor = new ConsultarPermisosByModuloInteractor();
            return _interactor.ConsultarPermisosPorModulo(request);
        }

        [Route("Set/Permission")]
        public GestionRolesResponse PostGestionPermission(GestionRolesRequest request)
        {
            GestionRolesInteractor _interactor = new GestionRolesInteractor();
            return _interactor.GestionRoles(request);
        }
    }
}
