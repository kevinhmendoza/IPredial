using System.Collections.Generic;

namespace Infrastructure.Initialization.Modules
{
    public class ModuloSeguridad
    {
        internal Menu Menu()
        {
            return new Menu
            {
                Titulo = "Seguridad",
                Descripcion = "Administrar usuarios",
                Habilitado = true,
                Icono = "fa fa-key",
                Modulo = "Seguridad",
                Rol = "Seguridad",
                SubMenu = new List<Menu>{
                    new Menu
                    {
                        Titulo = "Usuarios",
                        Descripcion = "Formulario para registrar modificar y consular los usuarios registrados en el sistema.",
                        Habilitado = true,
                        Modulo = "Registro/Usuario",
                        Rol = "SEGURIDADGestionUsuarios",
                        Prioridad = 100,
                    },
                    new Menu
                    {
                        Titulo = "Permisos",
                        Descripcion = "Permiso creado para asignar o quitar los permisos a los usuarios dentro del sistema.",
                        Habilitado = false,
                        Modulo = "-",
                        Rol = "SEGURIDADPermisosToUser",
                        Prioridad = 80,
                    },
                    new Menu
                    {
                        Titulo = "Cambiar Contraseña",
                        Descripcion = "Permiso creado para cambiar la contraseña a los usuarios registrados en el sistema.",
                        Habilitado = false,
                        Modulo = "-",
                        Rol = "SEGURIDADChangePasswordToUser",
                        Prioridad = 70,
                    },
                    new Menu
                    {
                        Titulo = "Confirmar Correo",
                        Descripcion = "Permiso creado para forzar la confirmación de correo electronico a los usuarios registrados en el sistema.",
                        Habilitado = false,
                        Modulo = "-",
                        Rol = "SEGURIDADForzeEmailConfirmToUser",
                        Prioridad = 70,
                    },
                    new Menu
                    {
                        Titulo = "Modificar Usuario",
                        Descripcion = "Permiso creado para modificar a los usuarios registrados en el sistema, SOLO su información basica (Tercero).",
                        Habilitado = false,
                        Modulo = "-",
                        Rol = "SEGURIDADModifiedTerceroAndUser",
                        Prioridad = 70,
                    },
                    new Menu
                    {
                        Titulo = "Asignar Pagina Inició",
                        Descripcion = "Permiso Establecer cual es la pagina de inicio del usuario.",
                        Habilitado = false,
                        Modulo = "-",
                        Rol = "SEGURIDADAssignIndexPageToUser",
                        Prioridad = 70,
                    },
                    new Menu
                    {
                        Titulo = "Auditoría",
                        Descripcion = "Formulario para consultar la auditoría del sistema, todas aquellas acciones que los usuarios realizan en el sistema.",
                        Habilitado = true,
                        Modulo = "Auditoria",
                        Rol = "SEGURIDADConsultaAuditoria",
                        Prioridad = 100,
                    }
                }
            };
        }
    }
}
