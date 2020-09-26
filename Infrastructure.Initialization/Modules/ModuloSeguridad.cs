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
                        Modulo = "Gestion/Usuario",
                        Rol = "Seguridad.GestionUsuarios",
                        Prioridad = 100,
                    },
                    new Menu
                    {
                        Titulo = "Permisos",
                        Descripcion = "Permiso creado para asignar o quitar los permisos a los usuarios dentro del sistema.",
                        Habilitado = false,
                        Modulo = "-",
                        Rol = "Seguridad.PermisosToUser",
                        Prioridad = 80,
                    },
                    //new Menu
                    //{
                    //    Titulo = "Cambiar Contraseña",
                    //    Descripcion = "Permiso creado para cambiar la contraseña a los usuarios registrados en el sistema.",
                    //    Habilitado = false,
                    //    Modulo = "-",
                    //    Rol = "Seguridad.ChangePasswordToUser",
                    //    Prioridad = 70,
                    //},
                    //new Menu
                    //{
                    //    Titulo = "Confirmar Correo",
                    //    Descripcion = "Permiso creado para forzar la confirmación de correo electronico a los usuarios registrados en el sistema.",
                    //    Habilitado = false,
                    //    Modulo = "-",
                    //    Rol = "Seguridad.ForzeEmailConfirmToUser",
                    //    Prioridad = 70,
                    //},
                    new Menu
                    {
                        Titulo = "Modificar Usuario",
                        Descripcion = "Permiso creado para modificar a los usuarios registrados en el sistema, SOLO su información basica (Tercero).",
                        Habilitado = false,
                        Modulo = "-",
                        Rol = "Seguridad.ModifiedTerceroAndUser",
                        Prioridad = 70,
                    },
                    //new Menu
                    //{
                    //    Titulo = "Asignar Pagina Inició",
                    //    Descripcion = "Permiso Establecer cual es la pagina de inicio del usuario.",
                    //    Habilitado = false,
                    //    Modulo = "-",
                    //    Rol = "Seguridad.AssignIndexPageToUser",
                    //    Prioridad = 70,
                    //},
                    //new Menu
                    //{
                    //    Titulo = "Auditoría",
                    //    Descripcion = "Formulario para consultar la auditoría del sistema, todas aquellas acciones que los usuarios realizan en el sistema.",
                    //    Habilitado = true,
                    //    Modulo = "Auditoria",
                    //    Rol = "Seguridad.ConsultaAuditoria",
                    //    Prioridad = 100,
                    //}
                }
            };
        }
    }
}
