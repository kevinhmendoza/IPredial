import { EstadoCuentaComponent } from '../pages/portal/estado-cuenta/estado-cuenta.component';
import { AuthLayoutComponent } from '../layouts/auth-layout/auth-layout.component';

export const Portal: any = {
  path: 'Portal', component: AuthLayoutComponent,
  children: [
    {
      path: 'Estado/Cuenta', component: EstadoCuentaComponent
    }
  ]
};
