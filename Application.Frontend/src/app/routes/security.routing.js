"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Seguridad = void 0;
var ngx_permissions_1 = require("ngx-permissions");
var gestion_usuarios_component_1 = require("../pages/security/gestion-usuarios/gestion-usuarios.component");
var admin_layout_component_1 = require("../layouts/admin-layout/admin-layout.component");
var modificar_usuario_component_1 = require("../pages/security/gestion-usuarios/modificar-usuario/modificar-usuario.component");
var gestion_permisos_component_1 = require("../pages/security/gestion-usuarios/gestion-permisos/gestion-permisos.component");
exports.Seguridad = {
    path: 'Seguridad', component: admin_layout_component_1.AdminLayoutComponent,
    children: [
        {
            path: 'Gestion/Usuario', component: gestion_usuarios_component_1.GestionUsuariosComponent, canActivate: [ngx_permissions_1.NgxPermissionsGuard],
            data: {
                permissions: {
                    only: ['Seguridad.GestionUsuarios'],
                    redirectTo: 'login'
                }
            }
        },
        {
            path: 'Permisos/User/:userName', component: gestion_permisos_component_1.GestionPermisosComponent, canActivate: [ngx_permissions_1.NgxPermissionsGuard],
            data: {
                permissions: {
                    only: ['Seguridad.PermisosToUser'],
                    redirectTo: 'Seguridad/Registro/Usuario'
                }
            }
        },
        {
            path: 'Modificar/User/Tercero/:terceroId', component: modificar_usuario_component_1.ModificarUsuarioComponent, canActivate: [ngx_permissions_1.NgxPermissionsGuard],
            data: {
                permissions: {
                    only: ['Seguridad.ModifiedTerceroAndUser'],
                    redirectTo: 'Seguridad/Registro/Usuario'
                }
            }
        }
    ]
};
//# sourceMappingURL=security.routing.js.map