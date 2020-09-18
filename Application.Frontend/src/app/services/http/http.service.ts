import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

import { Router } from '@angular/router';
import { AuthService } from '../security/auth.service';
import { SistemaService } from '../security/sistema.service';
import { Observable, throwError, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ApisRestBase } from '../apis';

@Injectable()
export class HttpService {

  constructor(private http: HttpClient, private _authService: AuthService) { }

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(url);
  }

  post<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(url, body);
  }

  put<T>(url: string, body: string): Observable<T> {
    return this.http.put<T>(url, body);
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(url);
  }

  patch<T>(url: string, body: string): Observable<T> {
    return this.http.patch<T>(url, body);
  }

  appendTokenFromUrl(url: string): string {
    return url + "?Authorization=" + this._authService.GetTokenUserLogged();
  }

}

export const ExcluirInterceptor = 'X-Skip-Interceptor';

@Injectable()
export class HttpServiceAllonimous {

  private headers: any;

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders().set(ExcluirInterceptor, '');
  }

  get<T>(url: string): Observable<T> {
    return this.http.get<T>(url, { headers: this.headers });
  }

  post<T>(url: string, body: any): Observable<T> {
    return this.http.post<T>(url, body, { headers: this.headers });
  }

  put<T>(url: string, body: string): Observable<T> {
    return this.http.put<T>(url, body, { headers: this.headers });
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(url, { headers: this.headers });
  }

  patch<T>(url: string, body: string): Observable<T> {
    return this.http.patch<T>(url, body, { headers: this.headers });
  }

}

@Injectable()
export class HttpServiceInterceptor implements HttpInterceptor {

  constructor(private _authService: SistemaService, private _router: Router, private _toastr: ToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!req.url.includes("http")) {
      req = req.clone({ url: ApisRestBase.UrlServer + req.url });
    }
    if (req.headers.has(ExcluirInterceptor)) {
      const headers = req.headers.delete(ExcluirInterceptor);
      return next.handle(req.clone({ headers }));
    }

    const token: string = this._authService.GetTokenUserLogged();

    const authReq = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + token)
    });

    const authFinally = authReq.clone({
      headers: authReq.headers.set('Content-Type', 'application/json')
    });

    return next.handle(authFinally).pipe(
      catchError((err: HttpErrorResponse) => this.handleError(err)
      ));
    //.catch(err => this.handleError(err));
  }

  private handleError(err: HttpErrorResponse): Observable<any> {

    if (err.error.Message) {
      this._toastr.error(err.error.Message, "Error!!");
    } else if (err.error && (!(err.error.error) || err.error.error && err.error.error != "invalid_grant")) {//Para que no muestre mensaje cuando ingresa
      this._toastr.error(err.error.toString(), "Error!!");
    }
    if (err.status === 401 || err.status === 403) {
      //this._authService.logout();//Este servicio redirecciona al login y cierra sesión
      this._authService.deleteCookieToken();//Este servicio redirecciona al login y cierra sesión
      //this._router.navigateByUrl('/login'); con esta forma lo redirecciona al login el login con el token en cockies lo manda al home
      return throwError(err.message);
    }

    return throwError(err);
  }
}

export class ShowMensajeErrorHttp {

  public GetMensaje(err: any): string {
    if (err.error.Message) {
      return err.error.Message;
    } else {
      return err.error.toString();
    }
  }
}


