import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../services/security/auth.service';
import { Router } from '@angular/router';
import { Login } from '../../entities/security/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  login: Login = {
    grant_type: "password",
    password: "",
    username: "",
    process: false,
  };

  public Year: number;

  constructor(private _service: AuthService, private _router: Router) {
    if (_service.UserLogged()) { this._router.navigate(['/Home']); }
    this.Year = new Date().getFullYear();
  }

  ngOnInit(): void {
    if (this._service.UserLogged()) {
      this._router.navigate(['/dashboard']);
    }
  }

  ngOnDestroy() {
  }


  Login(): void {
    this._service.obtainAccessToken(this.login);
  }

}
