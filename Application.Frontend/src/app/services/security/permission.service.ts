import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';

import { Observable } from 'rxjs';
import { Modulos, Permission } from '../../entities/security/modulos';
import { ApisRestUser } from '../apis';
import { UtilitiesAnibalService } from '../utilities';

@Injectable()
export class PermissionService {

  constructor(private _httpService: HttpService) { }

  public GetModulosWithPermission(): Observable<GetModulosWithPermissionResponse> {
    UtilitiesAnibalService.Console("Consultando roles");
    return this._httpService.get<GetModulosWithPermissionResponse>(ApisRestUser.GetPermissionUser);
  }
}


export class GetModulosWithPermissionResponse {
  public Modulos: Modulos[];
  public PermisoInicial: Permission;
    public LicenceActive: boolean;
    public Mensaje: string;
}
