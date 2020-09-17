using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Initialization.Interactors
{
    public class ConsultarModulosInteractor
    {
        private readonly ConsultarModulosResponse _response;
        private readonly SystemMenu _systemMenu;
        public ConsultarModulosInteractor()
        {
            _response = new ConsultarModulosResponse();
            _systemMenu = new SystemMenu();
        }

        public ConsultarModulosResponse ConsultarAllModules()
        {
            _response.Modulos= _systemMenu.Menus.Select(t => new ModuloDto()
            {
                Descripcion = t.Descripcion,
                Icono = t.Icono,
                MenuId = t.MenuId,
                Modulo = t.Modulo,
                Rol = t.Rol,
                Titulo = t.Titulo
            }).ToList();
            if (!_response.Modulos.Any())
            {
                _response.EstablecerError($"No se han inicializado los modulos en el sistema!!!");
            }
            return _response;
        }
    }

    public class ConsultarModulosResponse
    {
        public ConsultarModulosResponse()
        {
            Modulos = new List<ModuloDto>();
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public List<ModuloDto> Modulos { get; set; }
        public void EstablecerError(string mensaje)
        {
            Error = true;
            Mensaje += $"{mensaje}\n";
        }
    }

    public class ModuloDto
    {
        public string MenuId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Modulo { get; set; }
        public string Rol { get; set; }
    }
}
