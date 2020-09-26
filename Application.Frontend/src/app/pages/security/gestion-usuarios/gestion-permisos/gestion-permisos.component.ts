import { Component, OnInit, Input, Output, ViewChild, EventEmitter, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { GestionPermisosService, GetPermissionRequest, GestionPermissionRequest } from '../../../../services/security/gestionpermisos/gestion-permisos.service';
import { User } from '../../../../entities/security/user';
import { Modulos, Permission } from '../../../../entities/security/modulos';

@Component({
    selector: 'app-gestion-permisos',
    templateUrl: './gestion-permisos.component.html',
    styleUrls: ['./gestion-permisos.component.css'],
    providers: [ToastrService, GestionPermisosService]
})
export class GestionPermisosComponent implements OnInit {

  @Input() Usuario: User;

  public Modulos: Modulos[];
  public Permisos: Permission[];
    private _requestGetPermisos: GetPermissionRequest = { MenuId: "", UserName: "" };
    private _requestGuardarPermisos: GestionPermissionRequest = { Permisos: [], UserName: "" };
    public selectedAllPermission: any;
    constructor(
        private route: ActivatedRoute,
        private location: Location,
        private _toastr: ToastrService,
        private _gestionPermisosService: GestionPermisosService
    ) { }

    ngOnInit() {
      this._gestionPermisosService.GetDatosUsuario(this.route.snapshot.paramMap.get('userName'))
            .subscribe(response => {
                if (!response.Error) {
                    this._requestGetPermisos.UserName = response.Usuario.UserName;
                    this._requestGuardarPermisos.UserName = response.Usuario.UserName;
                    this.Usuario = response.Usuario;
                    this.ConsultarModulos();
                } else {
                    this._toastr.error(response.Mensaje, "Error!!");
                }
            });
    }

    public ActualizarPermisos(): void {
        this.Permisos.forEach((permiso: Permission, index: number) => {
            permiso.hasRol = this.selectedAllPermission;
        });
    }

    public GetPermission(menuId: string): void {
        this._requestGetPermisos.MenuId = menuId;
        this.ConsultarPermisos();
    }

    public GuardarPermisos(): void {
        this._requestGuardarPermisos.Permisos = this.Permisos;
        this.GestionarPermisos();
    }

    private GestionarPermisos(): void {
        this._gestionPermisosService.GuardarPermisos(this._requestGuardarPermisos).subscribe(response => {
            if (!response.Error) {
                this.ConsultarPermisos();
                this._toastr.success(response.Mensaje, "Correcto!!");
            } else {
                this._toastr.error(response.Mensaje, "Error!!");
            }
        });
    };

    private ConsultarModulos(): void {
        this._gestionPermisosService.GetModulos().subscribe(response => {
            if (!response.Error) {
                this.Modulos = response.Modulos;
                this._requestGetPermisos.MenuId = this.Modulos[0].MenuId;
                this.ConsultarPermisos();
            } else {
                this._toastr.error(response.Mensaje, "Error!!");
            }
        });
    };

    private ConsultarPermisos() {
        this.Permisos = [];
        this._gestionPermisosService.GetPermission(this._requestGetPermisos).subscribe(response => {
            if (!response.Error) {
                this.Permisos = response.Permisos;
            } else {
                this._toastr.error(response.Mensaje, "Error!!");
            }
        });
    };

}
