import { Component, OnInit, AfterContentInit } from '@angular/core';
import { Router } from '@angular/router';
import { SistemaService } from '../../services/security/sistema.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/security/auth.service';
import { PermissionService } from '../../services/security/permission.service';
import { LoadPermissionAppHelper } from '../../common/app-load/app-load.service';
import { NgxPermissionsService } from 'ngx-permissions';
import { Modulos } from '../../entities/security/modulos';


declare var jQuery: any;
declare var $: any;

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
     { path: '/dashboard', title: 'Dashboard',  icon: 'ni-tv-2 text-primary', class: '' },
    // { path: '/icons', title: 'Icons',  icon:'ni-planet text-blue', class: '' },
    // { path: '/maps', title: 'Maps',  icon:'ni-pin-3 text-orange', class: '' },
    //{ path: '/user-profile', title: 'Perfil',  icon:'ni-single-02 text-yellow', class: '' },
    // { path: '/tables', title: 'Tables',  icon:'ni-bullet-list-67 text-red', class: '' },
    // { path: '/login', title: 'Login',  icon:'ni-key-25 text-info', class: '' },
    // { path: '/register', title: 'Register',  icon:'ni-circle-08 text-pink', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  providers: [AuthService, PermissionService]
})
export class SidebarComponent implements OnInit, AfterContentInit {

  public menuItems: any[];
  public isCollapsed = true;

  constructor(private _router: Router, private _ngxPermissionsService: NgxPermissionsService, private _toastrService: ToastrService, private _authService: AuthService, private _permissionService: PermissionService) { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this._router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }

  public Logout(): void {
    this._authService.logout();
  }

  public Modulos: Array<Modulos> = new Array<Modulos>();

  private ConsultarPermisos() {
    this._permissionService.GetModulosWithPermission().subscribe(response => {
      let responseService = LoadPermissionAppHelper.MappedPermission(response, this._authService, this._toastrService);
      if (responseService) {
        this.Modulos = responseService.ModulosVisbles;
        this._ngxPermissionsService.loadPermissions(responseService.Permisos);
        if (response.PermisoInicial) {
          this._router.navigate([response.PermisoInicial.Url]);
        }
      }
    });

  }

  ngAfterContentInit() {
    if (this.Modulos.length <= 0) {
      this.ConsultarPermisos();
    }
  }

activeRoute(routename: string): boolean {
    let comparacion = this._router.url.indexOf(routename);

    return comparacion > -1;
  }

}
