"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Seguridad = void 0;
var ngx_permissions_1 = require("ngx-permissions");
var gestion_usuarios_component_1 = require("../pages/security/gestion-usuarios/gestion-usuarios.component");
var admin_layout_component_1 = require("../layouts/admin-layout/admin-layout.component");
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
        }
        //,
        //{
        //  path: 'Modificar/User/Tercero/:terceroId', component: ModificarUsuarioComponent, canActivate: [NgxPermissionsGuard],
        //  data: {
        //    permissions: {
        //      only: ['Seguridad.ModifiedTerceroAndUser'],
        //      redirectTo: 'Seguridad/Registro/Usuario'
        //    }
        //  }
        //}
    ]
};
//# sourceMappingURL=security.routing.js.map