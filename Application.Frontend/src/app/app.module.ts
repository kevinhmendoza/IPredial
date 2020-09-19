import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxPermissionsModule } from 'ngx-permissions';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { HttpServiceInterceptor, HttpService, HttpServiceAllonimous } from './services/http/http.service';
import { SistemaService } from './services/security/sistema.service';
import { ToastrModule } from 'ngx-toastr';
import { SecurityModule } from './pages/security/security.module';
import { AuthService } from './services/security/auth.service';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ComponentsModule,
    NgbModule,
    RouterModule,
    AppRoutingModule,
    SecurityModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      progressBar: true,
      positionClass: 'toast-bottom-center',
      //preventDuplicates: true,
    }),
    NgxPermissionsModule.forRoot()
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    AuthLayoutComponent
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'es-CO' // 'de-DE' for Germany, 'fr-FR' for France ...
    },
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpServiceInterceptor,
      multi: true
    },
    HttpService,
    AuthService,
    HttpServiceAllonimous,
    SistemaService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
