import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { UtilitiesAnibalService } from '../utilities';
import { ApisRestGestionUsers } from '../apis';
import { Tercero } from '../../entities/security/tercero';
import { User } from '../../entities/security/user';

@Injectable({
  providedIn: 'root'
})
export class GestionUsuariosService {

  constructor(private _httpService: HttpService) { }

  public GetAllUsuarios(): Observable<GetAllUsersResponse> {
    UtilitiesAnibalService.Console("Consultando Usuarios Registrados");
    return this._httpService.get<GetAllUsersResponse>(ApisRestGestionUsers.GetAllUsers);
  }

  public RegistrarUsuario(request: RegistroUsuarioRequest): Observable<any> {
    UtilitiesAnibalService.Console("Registrando Usuario");
    return this._httpService.post<any>(ApisRestGestionUsers.PostRegisterUser, request);
  }

  public ToggleUsuario(request: ToggleUsuarioRequest ): Observable<any> {
    UtilitiesAnibalService.Console("Inactivar/Desactivar Usuario");
    return this._httpService.post<any>(ApisRestGestionUsers.PostToggleUser, request);
  }
}

export class ToggleUsuarioRequest {
  public UserName: string;
  public TerceroId: number;
  public Estado: string;
}


export class GetAllUsersResponse {
  public Usuarios: Array<User>;
}

export class RegistroUsuarioRequest {
  constructor() {
    this.Tercero = new Tercero();
    this.Usuario = new UsuarioRequest();
  }
  Tercero: Tercero;
  Usuario: UsuarioRequest;
}

export class UsuarioRequest {
  Email: string;
  FechaDesactivacion: Date;
  Password: string;
  PasswordConfirm: string;
  UserName: string;
}
