import { Injectable, Injector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgxPermissionsService } from 'ngx-permissions';
import { HttpServiceAllonimous } from '../../services/http/http.service';
import { AuthService } from '../../services/security/auth.service';
import { PermissionService, GetModulosWithPermissionResponse } from '../../services/security/permission.service';
import { ApisRestBase } from '../../services/apis';
import { UtilitiesAnibalService } from '../../services/utilities';
import { Modulos, Permission } from '../../entities/security/modulos';

@Injectable()
export class AppLoadService {

  constructor(private injector: Injector) { }

  getHttpServiceAllonimous() {
    return this.injector.get(HttpServiceAllonimous);
  }

  getToastrService() {
    return this.injector.get(ToastrService);
  }

  getAuthService() {
    return this.injector.get(AuthService);
  }

  getPermissionService() {
    return this.injector.get(PermissionService);
  }

  getNgxPermissionsService() {
    return this.injector.get(NgxPermissionsService);
  }

  getHttp() {
    return this.injector.get(HttpServiceAllonimous);
  }

  async initializeApp() {
    let data = await this.getHttp().get<any>('./config/app.config.json')
      .toPromise();
    ApisRestBase.UrlServer = data.App.UrlBase;
    UtilitiesAnibalService.DomainServer = data.App.UrlBase;
    return this.getSettings();
  }

  getSettings(): Promise<any> {
    let authService = this.getAuthService();
    if (authService.UserLogged()) {
      const promise = this.getPermissionService().GetModulosWithPermission()
        .toPromise()
        .then(settings => {
          let response = LoadPermissionAppHelper.MappedPermission(settings, authService, this.getToastrService());
          if (response) this.getNgxPermissionsService().loadPermissions(response.Permisos);
        })
        .catch(error => {
        });

      return promise;
    }
    UtilitiesAnibalService.Console("No esta autenticado");
  }
}

export class LoadPermissionAppHelper {
  public static MappedPermission(response: GetModulosWithPermissionResponse, _serviceAuth: AuthService, _toastr: ToastrService): LoadPermissionAppHelperResponse {
    if (response.Modulos.length > 0) {
      let Permisos: string[] = [];
      let ModulosVisbles: Modulos[] = [];
      let contador = 0;
      for (let modulo of response.Modulos) {
        let PermissionModuleActual: Permission[] = [];
        for (let permiso of modulo.SubMenu) {
          if (permiso.Habilitado) {
            PermissionModuleActual.push(permiso);
          }
          Permisos.push(permiso.Rol);
        }
        ModulosVisbles.push(modulo);
        ModulosVisbles[contador].SubMenu = PermissionModuleActual;
        contador++;
      }
      let responseMethod = new LoadPermissionAppHelperResponse();
      responseMethod.ModulosVisbles = ModulosVisbles;
      responseMethod.Permisos = Permisos;
      return responseMethod;
    } else {
      _toastr.error("Estimado usuario " + _serviceAuth.GetNombreTerceroLogged() + " usted no tiene permisos asignados", "Sin Permisos!!");
      _serviceAuth.logout();
    }
    return null;
  }
}

export class LoadPermissionAppHelperResponse {
  public Permisos: string[] = [];
  public ModulosVisbles: Modulos[] = [];
}
