import { Injectable } from '@angular/core';
import { EstadoCuenta } from '../../entities/general/estado-cuenta';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';
import { UtilitiesAnibalService } from '../utilities';
import { ApisRestEstadoCuenta } from '../apis';

@Injectable({
  providedIn: 'root'
})
export class EstadoCuentaService {

  constructor(private _httpService: HttpService) { }

  public GetEstadoCuenta(request: EstadoCuentaRequest): Observable<EstadoCuentaResponse> {
    UtilitiesAnibalService.Console("Consultando estado de cuenta");
    return this._httpService.post<EstadoCuentaResponse>(ApisRestEstadoCuenta.PostEstadoCuenta, request);
  }
}

export class EstadoCuentaRequest {
  public TipoFiltro: string;
  public Filtro: string;
}


export class EstadoCuentaResponse {
  public EstadoCuenta: Array<EstadoCuenta>;
}
