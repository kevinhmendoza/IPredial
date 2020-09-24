import { Component, OnInit, LOCALE_ID, Inject } from '@angular/core';
import { AuthService } from '../../../services/security/auth.service';
import { Router } from '@angular/router';
import { Login } from '../../../entities/security/login';
import { EstadoCuentaService, EstadoCuentaRequest, EstadoCuentaTable } from '../../../services/portal/estado-cuenta.service';
import { ToastrService } from 'ngx-toastr';
import { CurrencyPipe } from '@angular/common';
import { formatCurrency } from '@angular/common';

@Component({
  selector: 'app-estado-cuenta',
  templateUrl: './estado-cuenta.component.html',
  styleUrls: ['./estado-cuenta.component.css'],
  providers: [EstadoCuentaService]
})
export class EstadoCuentaComponent implements OnInit {
  public Request: EstadoCuentaRequest = new EstadoCuentaRequest();
  

  constructor(private _estadoCuentaService: EstadoCuentaService, private _toastr: ToastrService, private cp: CurrencyPipe) {
  }

  ngOnInit(): void {

  }

  public Columnas: Array<any> = [
    { title: 'Identifiacion Propietario', name: 'IdentifiacionPropietario' },
    { title: 'Propietario', name: 'Propietario' },
    { title: 'Referencia Catastral', name: 'ReferenciaCatastral' },
    { title: 'Direccion', name: 'Direccion' },
    { title: 'Avaluo', name: 'Avaluo' },
    { title: 'Area Terreno', name: 'AreaTerreno' },
    { title: 'Area Construida', name: 'AreaConstruida' },
    { title: 'Clase', name: 'Clase' },
    { title: 'Estrato', name: 'Estrato' },
    { title: 'Destino Economico', name: 'DestinoEconomico' },
    { title: 'Numero Liquidacion', name: 'NumeroLiquidacion' },
    { title: 'Vigencia', name: 'Vigencia' },
    { title: 'Periodo', name: 'Periodo' },
    { title: 'Valor Capital', name: 'ValorCapitalCurrency' },
    { title: 'Valor Interes', name: 'ValorInteresCurrency' },
    { title: 'Total', name: 'TotalCurrency' }
  ];

  public MostrarTabla: boolean = false;
  public CargandoTablaEstadoCuenta: boolean = false;
  public EstadoCuenta: Array<EstadoCuentaTable> = new Array<EstadoCuentaTable>();
  Consultar(): void {
    if (!this.Request.TipoFiltro) {
      this._toastr.warning("Debe especificar el tipo de filtro de busqueda");
      return;
    }
    if (!this.Request.Filtro) {
      this._toastr.warning("Debe especificar el filtro de busqueda");
      return;
    }
    this.CargandoTablaEstadoCuenta = true;
    this._estadoCuentaService.GetEstadoCuenta(this.Request).subscribe(response => {
      this.MostrarTabla = true;
      this.CargandoTablaEstadoCuenta = false;
      this.EstadoCuenta = response.EstadoCuenta;
      const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
        minimumFractionDigits: 2
      })
      for (let aux of this.EstadoCuenta) {
        aux.TotalCurrency = formatter.format(aux.Total)
        aux.ValorCapitalCurrency = formatter.format(aux.ValorCapital)
        aux.ValorInteresCurrency = formatter.format(aux.ValorInteres)
      }
    });
  }


  LimpiarFiltro(): void {

  
    this.Request.Filtro = "";

  }

}
