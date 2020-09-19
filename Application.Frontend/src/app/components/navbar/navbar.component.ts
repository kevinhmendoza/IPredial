import { Component, OnInit} from '@angular/core';
import { ROUTES } from '../sidebar/sidebar.component';
import { Location} from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/security/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  providers: [AuthService]
})
export class NavbarComponent implements OnInit {
  public focus;
  public listTitles: any[];
  public location: Location;

  constructor(location: Location, private _router: Router, private _toastrService: ToastrService, private _authService: AuthService) {
    this.location = location;
  }

  ngOnInit() {
    this.listTitles = ROUTES.filter(listTitle => listTitle);
    if (!this._authService.UserLogged()) {
      this._router.navigate(['/login']);
    }
  }
  getTitle(){
    var titlee = this.location.prepareExternalUrl(this.location.path());
    if(titlee.charAt(0) === '#'){
        titlee = titlee.slice( 1 );
    }

    for(var item = 0; item < this.listTitles.length; item++){
        if(this.listTitles[item].path === titlee){
            return this.listTitles[item].title;
        }
    }
    return '';
  }

  public getUserName(): string {
    return this._authService.GetNombreTerceroLogged();
  }


  public Logout(): void {
    this._authService.logout();
  }


}
