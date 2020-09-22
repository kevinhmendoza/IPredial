import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EstadoCuentaComponent } from './estado-cuenta/estado-cuenta.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClipboardModule } from 'ngx-clipboard';
import { IPredialCommonModule } from '../../common/ipredial-common.module';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [EstadoCuentaComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    IPredialCommonModule,
    ModalModule.forRoot(),
  ]
})
export class PortalModule { }
