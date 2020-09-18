import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Cookie } from 'ng2-cookies';


@Injectable()
export class SistemaService {

    constructor(private _router: Router) {
    }

    public saveToken(token) {
        var expireDate = new Date().getTime() + (1000 * token.expires_in);
        Cookie.set("access_token", token.access_token, expireDate);
        Cookie.set("username", token.userName, expireDate);
        Cookie.set("nombreTercero", token.Tercero, expireDate);
        console.log('Obtained Access token');
        this._router.navigate(['/dashboard']);
    }

    public GetUserNameLogged(): string {
        return Cookie.get('username');
    }

    public GetNombreTerceroLogged(): string {
        return Cookie.get('nombreTercero');
    }

    public GetTokenUserLogged(): string {
        return Cookie.get('access_token');
    }

    public UserLogged(): boolean {
        return Cookie.check('access_token');
    }

    public checkCredentials() {
        if (!this.UserLogged()) {
            this._router.navigate(['/login']);
        }
    }

    public hasPermission(permission: string): boolean {
        //TODO: implement
      return false;
    }

    public deleteCookieToken() {
        Cookie.delete('access_token');
        this._router.navigate(['/login']);
    }

}
