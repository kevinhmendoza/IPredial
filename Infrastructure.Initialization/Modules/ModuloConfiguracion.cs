using System.Collections.Generic;

namespace Infrastructure.Initialization.Modules
{
    public class ModuloConfiguracion
    {
        internal Menu Menu()
        {
            return new Menu
            {
                Titulo = "Configuracion",
                Descripcion = "Administración de datos basicos",
                Habilitado = true,
                Icono = "fa fa-cogs",
                Modulo = "Configuracion",
                Rol = "Configuracion",
                SubMenu = new List<Menu>{
                   
                }
            };
        }
    }
}
