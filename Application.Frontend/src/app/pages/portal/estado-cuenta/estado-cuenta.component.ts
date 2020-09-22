import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/security/auth.service';
import { Router } from '@angular/router';
import { Login } from '../../../entities/security/login';
import { EstadoCuentaService, EstadoCuentaRequest } from '../../../services/portal/estado-cuenta.service';
import { EstadoCuenta } from '../../../entities/general/estado-cuenta';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-estado-cuenta',
  templateUrl: './estado-cuenta.component.html',
  styleUrls: ['./estado-cuenta.component.css'],
  providers: [EstadoCuentaService]
})
export class EstadoCuentaComponent implements OnInit{
  public Request: EstadoCuentaRequest = new EstadoCuentaRequest();


  constructor(private _estadoCuentaService: EstadoCuentaService, private _toastr: ToastrService) {
  }

  ngOnInit(): void {
    
  }

  public Columnas: Array<any> = [
    { title: 'ReferenciaCatastral', name: 'ReferenciaCatastral' },
    { title: 'IdentifiacionPropietario', name: 'IdentifiacionPropietario' },
    { title: 'Propietario', name: 'Propietario' },
    { title: 'Direccion', name: 'Direccion' },
    { title: 'Avaluo', name: 'Avaluo' },
    { title: 'AreaTerreno', name: 'AreaTerreno' },
    { title: 'AreaConstruida', name: 'AreaConstruida' },
    { title: 'Clase', name: 'Clase' },
    { title: 'Estrato', name: 'Estrato' },
    { title: 'DestinoEconomico', name: 'DestinoEconomico' },
    { title: 'UsoSuelo', name: 'UsoSuelo' },
    { title: 'NumeroLiquidacion', name: 'NumeroLiquidacion' },
    { title: 'Vigencia', name: 'Vigencia' },
    { title: 'Periodo', name: 'Periodo' },
    { title: 'ValorCapital', name: 'ValorCapital' },
    { title: 'ValorInteres', name: 'ValorInteres' },
    { title: 'Total', name: 'Total' }
  ];


  public EstadoCuenta: Array<EstadoCuenta> = new Array<EstadoCuenta>();
  Consultar(): void {
    if (!this.Request.TipoFiltro) {
      this._toastr.warning("Debe especificar el tipo de filtro de busqueda");
      return;
    }
    if (!this.Request.Filtro) {
      this._toastr.warning("Debe especificar el filtro de busqueda");
      return;
    }
    this._estadoCuentaService.GetEstadoCuenta(this.Request).subscribe(response => {
      this.EstadoCuenta = response.EstadoCuenta;
    });
  }

}
