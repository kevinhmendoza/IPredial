import { Injectable } from '@angular/core';
import { HttpService } from '../../http/http.service';

import { Observable } from 'rxjs';
import { UtilitiesAnibalService } from '../../utilities';
import { ApisRestGestionPermisos } from '../../apis';
import { User } from '../../../entities/security/user';
import { Permission, Modulos } from '../../../entities/security/modulos';

@Injectable()
export class GestionPermisosService {

  constructor(private _httpService: HttpService) { }

  public GetDatosUsuario(userName: string): Observable<GetUserResponse> {
    UtilitiesAnibalService.Console("Consultando Usuario");
    return this._httpService.get<GetUserResponse>(ApisRestGestionPermisos.GetUser + "/" + userName);
  }

  public GetModulos(): Observable<GetModulesResponse> {
    UtilitiesAnibalService.Console("Consultando Modulos");
    return this._httpService.get<GetModulesResponse>(ApisRestGestionPermisos.GetModules);
  }

  public GetPermission(_request: GetPermissionRequest): Observable<GetPermissionResponse> {
    UtilitiesAnibalService.Console("Consultando Permisos");
    return this._httpService.post<GetPermissionResponse>(ApisRestGestionPermisos.PostGetPermission, _request);
  }

  public GuardarPermisos(_request: GestionPermissionRequest): Observable<GestionPermissionResponse> {
    UtilitiesAnibalService.Console("Guardando Permisos");
    return this._httpService.post<GestionPermissionResponse>(ApisRestGestionPermisos.PostGestionPermission, _request);
  }

}

export class ResponseGeneric {
  public Error: boolean;
  public Mensaje: string;
}


export class GetUserResponse extends ResponseGeneric{
  public Usuario: User;
}


export class GetPermissionRequest {
  public UserName: string;
  public MenuId: string;
}

export class GestionPermissionRequest {
  public UserName: string;
  public Permisos: Permission[];
}

export class GestionPermissionResponse extends ResponseGeneric{

}

export class GetPermissionResponse extends ResponseGeneric{
  public Permisos: Permission[];
}

export class GetModulesResponse extends ResponseGeneric{
  public Modulos: Modulos[];
}
