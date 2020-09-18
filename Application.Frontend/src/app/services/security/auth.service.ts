import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { SistemaService } from './sistema.service';
import { Login } from '../../entities/security/login.component';
import { ApisRestUser } from '../apis';

@Injectable()
export class AuthService {

  constructor(private _router: Router, private _sistemaService: SistemaService, private _http: HttpClient, private toastr: ToastrService) {
  }

  public obtainAccessToken(loginData: Login) {
    const params = new HttpParams()
      .set('username', loginData.username)
      .set('password', loginData.password)
      .set('grant_type', loginData.grant_type);


    let headers = new HttpHeaders({
      'Content-type': 'application/x-www-form-urlencoded; charset=utf-8;'
    });//, 'Authorization': 'Basic ' + btoa("fooClientIdPassword:secret") });
    loginData.process = true;
    let request = params.toString();
    this._http.post(ApisRestUser.PostLogin, request, { headers: headers })
      .subscribe(
        data => {
          this._sistemaService.saveToken(data);
          loginData.process = false;
        },
        err => {
          this.ControlarError(err);
          loginData.process = false;
        });
  }

  private ControlarError(response) {
    //var json = JSON.parse(response._body);
    this.toastr.warning(response.error.error_description, 'Credenciales Incorrectas');
  }

  public GetUserNameLogged(): string {
    return this._sistemaService.GetUserNameLogged();
  }

  public GetNombreTerceroLogged(): string {
    return this._sistemaService.GetNombreTerceroLogged();
  }

  public GetTokenUserLogged(): string {
    return this._sistemaService.GetTokenUserLogged();
  }

  public UserLogged(): boolean {
    return this._sistemaService.UserLogged();
  }

  public checkCredentials() {
    this._sistemaService.checkCredentials();
  }

  public hasPermission(permission: string): boolean {
    //TODO: implement
    return false;
  }

  public logout() {

    let token = 'Bearer ' + this.GetTokenUserLogged();

    var headers = new HttpHeaders({ 'Content-type': 'application/json;', 'Authorization': token });


    this._http.post(ApisRestUser.PostLogout, null, { headers: headers })
      .subscribe(
      );
    this._sistemaService.deleteCookieToken();
  }

}
