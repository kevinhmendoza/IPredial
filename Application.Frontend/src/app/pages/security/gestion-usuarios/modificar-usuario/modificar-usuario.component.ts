import { Component, OnInit, Input, Output, ViewChild, EventEmitter, SimpleChanges } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ModificarUsuarioService, ModificarUsuarioAndTerceroRequest, TerceroUserUpdate } from '../../../../services/security/modificarusuario/modificar-usuario.service';
import { Tercero } from '../../../../entities/security/tercero';
import { User } from '../../../../entities/security/user';

@Component({
  selector: 'app-modificar-usuario',
  templateUrl: './modificar-usuario.component.html',
  styleUrls: ['./modificar-usuario.component.css'],
  providers: [ToastrService, ModificarUsuarioService]
})
export class ModificarUsuarioComponent implements OnInit {

  public Usuario: User;
  public Tercero: Tercero;

  private Request: ModificarUsuarioAndTerceroRequest;

  public disabledButton: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private _toastr: ToastrService,
    private _modificarUsuarioService: ModificarUsuarioService
  ) { }

  ngOnInit() {
    this._modificarUsuarioService.ConsultarTerceroyUsuario(this.route.snapshot.paramMap.get('terceroId'))
      .subscribe(response => {

        if (!response.Error) {
          this.Tercero = response.Tercero;
          this.Usuario = response.Usuario;
          console.log(response);
        } else {
          this._toastr.error(response.Mensaje, "Error!!");
        }
      });
  }

  public ModificarTercero(requestForm): void {
    this.MapearTerceroToRequest();
    this.disabledButton = true;
    setTimeout(() => {
      this.disabledButton = false;
    }, 2500);
    this._modificarUsuarioService.ModificarTercero(this.Request).subscribe(response => {
      if (!response.Error) {
        this._toastr.success(response.Mensaje, "Correcto!");
        setTimeout(() => {
          window.history.back();
        }, 1500);
      } else {
        this._toastr.error(response.Mensaje, "Advertencia!");
      }
    });
  }


  private MapearTerceroToRequest() {
    this.Request = {
      Tercero: this.Tercero as TerceroUserUpdate
    }
  }

  public CambioIdentificacion(identificacion): void {
    this.Usuario.UserName = identificacion;
  }

  public CambioCorreoElectronico(correoelectronico): void {
    this.Usuario.Email = correoelectronico;
  }
}
