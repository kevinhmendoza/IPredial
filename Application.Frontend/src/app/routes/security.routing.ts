import { NgxPermissionsGuard } from 'ngx-permissions';

import { GestionUsuariosComponent } from "../pages/security/gestion-usuarios/gestion-usuarios.component";
import { AdminLayoutComponent } from "../layouts/admin-layout/admin-layout.component";

export const Seguridad: any = {
  path: 'Seguridad', component: AdminLayoutComponent,
  children: [
    {
      path: 'Gestion/Usuario', component: GestionUsuariosComponent, canActivate: [NgxPermissionsGuard],
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
