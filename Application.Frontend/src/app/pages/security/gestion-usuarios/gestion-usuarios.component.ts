import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User } from '../../../entities/security/user';
import { RegistroUsuarioRequest, GestionUsuariosService } from '../../../services/security/gestion-usuarios.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-gestion-usuarios',
  templateUrl: './gestion-usuarios.component.html',
  styleUrls: ['./gestion-usuarios.component.css']
})
export class GestionUsuariosComponent implements OnInit {


  constructor(
    private _gestionUsersService: GestionUsuariosService,
    private _toastr: ToastrService,
    private _modalService: BsModalService,
    private _router: Router) {

  }

  ngOnInit() {
    this.ConsultarUsuarios();
    this.UsuarioRequest.Tercero.Pais = "Colombia";
  }
  public disabledButton: boolean = false;
  private modalRef: BsModalRef;

  public Usuarios: User[];
  public CargandoTablaUsuarios: boolean = true;
  public UsuarioRequest: RegistroUsuarioRequest = new RegistroUsuarioRequest();


  private ConsultarUsuarios(): void {
    this.CargandoTablaUsuarios = true;
    this._gestionUsersService.GetAllUsuarios().subscribe(response => {
      this.CargandoTablaUsuarios = false;
        this.Usuarios = response.Usuarios;
        
    }, err => {
      this.CargandoTablaUsuarios = false;
    });

  }

  public OpenModal(template: TemplateRef<any>) {
    this.disabledButton = false;
    this.modalRef = this._modalService.show(template, { class:"modal-lg"});
  }

  public RegistrarUsuario(user): void {
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

  public IrFormularioEditar(user: User): void{
    this._router.navigate(['/Seguridad/Modificar/User/Tercero/' + user.TerceroId]);
  }

}
