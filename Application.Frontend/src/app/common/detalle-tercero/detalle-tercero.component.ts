import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import * as moment from 'moment';
import { Tercero } from '../../entities/security/tercero';

@Component({
  selector: 'app-detalle-tercero',
  templateUrl: './detalle-tercero.component.html',
  styleUrls: ['./detalle-tercero.component.css']
})
export class DetalleTerceroComponent implements OnInit {

  ngOnInit() {
  }

  private _Tercero: Tercero;
  @Input()
  set Tercero(tercero: Tercero) {
    this._Tercero = tercero;
    if (this._Tercero.FechaExpedicion) {
      this._Tercero.FechaExpedicion = moment(this._Tercero.FechaExpedicion).format("YYYY-MM-DD");
    }
    if (this._Tercero.FechaNacimiento) {
      this._Tercero.FechaNacimiento = moment(this._Tercero.FechaNacimiento).format("YYYY-MM-DD");
    }
  }

  get Tercero(): Tercero {
    return this._Tercero;
  }

  private _DeshabilitarInputs: boolean;
  @Input()
  set DeshabilitarInputs(deshabilitar: boolean) {
    this._DeshabilitarInputs = deshabilitar;
  }

  get DeshabilitarInputs(): boolean {
    return this._DeshabilitarInputs;
  }

  private _Identificacion: string;
  @Input()
  set Identificacion(identificacion: string) {
    if (identificacion) {
      this._Identificacion = identificacion;
    }
  }

  get Identificacion(): string {
    return this._Identificacion;
  }

  @Output() public GetTercero: EventEmitter<Tercero> = new EventEmitter();

  @Output() public ChangeIdentificacion = new EventEmitter();

  public CambioIdentificacion($event): void {
    this.ChangeIdentificacion.emit(this.Tercero.Identificacion);
  }

  @Output() public ChangeCorreoElectronico = new EventEmitter();

  public CambioCorreoElectronico($event): void {
    this.ChangeCorreoElectronico.emit(this.Tercero.CorreoElectronico);
  }
}
