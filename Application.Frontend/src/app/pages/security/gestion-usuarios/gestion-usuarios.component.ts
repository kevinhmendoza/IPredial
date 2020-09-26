import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User } from '../../../entities/security/user';
import { RegistroUsuarioRequest, GestionUsuariosService, UserTable } from '../../../services/security/gestion-usuarios.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ConfirmationDialogService } from '../../../common/confirmation-dialog/confirmation-dialog.service';
import { CellClickedTableGeneric } from '../../../common/data-table-generic/data-table-generic.component';

@Component({
  selector: 'app-gestion-usuarios',
  templateUrl: './gestion-usuarios.component.html',
  styleUrls: ['./gestion-usuarios.component.css'],
  providers: [ConfirmationDialogService]
})
export class GestionUsuariosComponent implements OnInit {


  constructor(
    private _gestionUsersService: GestionUsuariosService,
    private _toastr: ToastrService,
    private _modalService: BsModalService,
    private _router: Router,
    private _confirmationDialogService: ConfirmationDialogService) {

  }

  public Columnas: Array<any> = [
    { title: 'Id', name: 'TerceroId', classNameRow: 'text-right' },
    { title: 'Identificación', className: 'text-center', classNameRow: 'text-right', name: 'Identificacion' },
    { title: 'Nombre Completo', name: 'NombreCompleto', },
    { title: 'Email', name: 'Email' },
    { title: 'Usuario', name: 'UserName' },
    { title: 'Correo Confirmado', name: 'CorreoConfirmado', classNameRow: 'text-center', replaceNameInExportFor: 'CorreoConfirmado' },
    { title: 'Estado', name: 'Estado', classNameRow: 'text-center', replaceNameInExportFor: 'Estado' },
    { title: '', name: 'ModificarUser' },
    { title: '', name: 'ToggleUser' },
    { title: '', name: 'PermissionUser' },
  ];

  ngOnInit() {
    this.ConsultarUsuarios();
    this.UsuarioRequest.Tercero.Pais = "Colombia";
  }
  public disabledButton: boolean = false;
  private modalRef: BsModalRef;

  public Usuarios: UserTable[];
  public CargandoTablaUsuarios: boolean = true;
  public UsuarioRequest: RegistroUsuarioRequest = new RegistroUsuarioRequest();


  private ConsultarUsuarios(): void {
    this.CargandoTablaUsuarios = true;
    this._gestionUsersService.GetAllUsuarios().subscribe(response => {
      this.CargandoTablaUsuarios = false;
      this.Usuarios = response.Usuarios;
      for (let usuario of this.Usuarios) {
        usuario.ModificarUser = '<a href="javascript:void(0);" class="btn btn-sm btn-neutral">Editar</a>';
        if (usuario.Estado == "AC") {
          usuario.ToggleUser = '<a href="javascript:void(0);" class="btn btn-sm btn-neutral">Inactivar</a>';
        } else {
          usuario.ToggleUser = '<a href="javascript:void(0);" class="btn btn-sm btn-neutral">Activar</a>';
        }
        usuario.PermissionUser = '<a href="javascript:void(0);" class="btn btn-sm btn-neutral">Permisos</a>';
        usuario.CorreoConfirmado = "NO";
        if (usuario.EmailConfirmed) {
          usuario.CorreoConfirmado = "SI";
        }
      }
    }, err => {
      this.CargandoTablaUsuarios = false;
    });

  }

  public OpenModal(template: TemplateRef<any>) {
    this.disabledButton = false;
    this.modalRef = this._modalService.show(template, { class: "modal-lg" });
  }

  public ToggleUsuario(user: UserTable): void {
    this._confirmationDialogService.confirm('Confirmación', "Esta seguro de continuar con el proceso?").subscribe(respuesta => {
      if (respuesta) {
        this.disabledButton = true;
        var estado = "AC";
        if (user.Estado == "AC") {
          var estado = "IN";
        }
        this._gestionUsersService.ToggleUsuario({ Estado: estado, TerceroId: user.TerceroId, UserName: user.UserName }).subscribe(response => {
          if (!response.Error) {
            this._toastr.success(response.Mensaje, "Correcto!");
            this.ConsultarUsuarios();
          } else {
            this._toastr.error(response.Mensaje, "Advertencia!");
          }
        }, err => {
        });
      }
    });
  }

  public ClickFila(data: CellClickedTableGeneric): any {
    if (data.Columna === "ModificarUser") {
      this.IrFormularioEditar(data.Fila);
    } else if (data.Columna === "ToggleUser") {
      this.ToggleUsuario(data.Fila);
    } else if (data.Columna === "PermissionUser") {
      this.IrFormularioPermisos(data.Fila);
    }
  }

  public RegistrarUsuario(user): void {
    this._confirmationDialogService.confirm('Confirmación', "Esta seguro de continuar con el proceso?").subscribe(respuesta => {
      if (respuesta) {
        this.disabledButton = true;
        this._gestionUsersService.RegistrarUsuario(this.UsuarioRequest).subscribe(response => {
          this.disabledButton = false;
          if (!response.Error) {
            this.modalRef.hide();
            this._toastr.success(response.Mensaje, "Correcto!");
            this.ConsultarUsuarios();
          } else {
            this._toastr.error(response.Mensaje, "Advertencia!");
          }
        }, err => {
          this.disabledButton = false;
        });
      }
    });
  }

  public CambioIdentificacion(identificacion): void {
    this.UsuarioRequest.Usuario.UserName = identificacion;
  }

  public CambioCorreoElectronico(correoelectronico): void {
    this.UsuarioRequest.Usuario.Email = correoelectronico;
  }

  public GenerarPassword(): void {
    if (this.UsuarioRequest.Tercero.Identificacion && this.UsuarioRequest.Tercero.TipoIdentificacion) {
      this.UsuarioRequest.Usuario.Password = this.UsuarioRequest.Tercero.TipoIdentificacion[0].toUpperCase() + this.UsuarioRequest.Tercero.TipoIdentificacion[0].toLowerCase() + this.UsuarioRequest.Tercero.Identificacion + "*";
      this.UsuarioRequest.Usuario.PasswordConfirm = this.UsuarioRequest.Usuario.Password;
    } else {
      this._toastr.warning("Debe digitar la identificación y tipo de identificación del tercero");
    }
  }

  public IrFormularioEditar(user: User): void {
    this._router.navigate(['/Seguridad/Modificar/User/Tercero/' + user.TerceroId]);
  }

  public IrFormularioPermisos(user: User): void {
    this._router.navigate(['/Seguridad/Permisos/User/' + user.UserName]);
  }

}
