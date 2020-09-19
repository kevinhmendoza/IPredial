import { Injectable } from '@angular/core';
import { HttpService } from '../../http/http.service';

import { Observable } from 'rxjs';
import { Tercero } from '../../../entities/security/tercero';
import { User } from '../../../entities/security/user';
import { UtilitiesAnibalService } from '../../utilities';
import { ApisRestModificarUserAndTercero } from '../../apis';

@Injectable()
export class ModificarUsuarioService {

  constructor(private _httpService: HttpService) { }

  public ConsultarTerceroyUsuario(TerceroId: string): Observable<ConsultaUserAndTerceroResponse> {
    UtilitiesAnibalService.Console("Consultando tercero and usuario");
    return this._httpService.get<ConsultaUserAndTerceroResponse>(ApisRestModificarUserAndTercero.GetTerceroAndUser + "/" + TerceroId);
  }

  public ModificarTercero(Request: ModificarUsuarioAndTerceroRequest): Observable<ModificarUsuarioAndTerceroResponse> {
    Request.Tercero.UserName = Request.Tercero.Identificacion;
    Request.Tercero.Email = Request.Tercero.CorreoElectronico;
    UtilitiesAnibalService.Console("Modificando tercero");
    return this._httpService.post<ModificarUsuarioAndTerceroResponse>(ApisRestModificarUserAndTercero.PostModificarTercero, Request.Tercero);
  }
}

export class ResponseGeneric {
  public Error: boolean;
  public Mensaje: string;
}


export class ModificarUsuarioAndTerceroRequest {
  public Tercero: TerceroUserUpdate ;
}

export class ModificarUsuarioAndTerceroResponse extends ResponseGeneric {

}

export class TerceroUserUpdate extends Tercero{
  public Email: string;
  public UserName: string;
}

export class ConsultaUserAndTerceroResponse extends ResponseGeneric {
  public Tercero: Tercero;
  public Usuario: User;
}
