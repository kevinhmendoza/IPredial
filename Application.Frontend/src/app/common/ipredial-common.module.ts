import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClipboardModule } from 'ngx-clipboard';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DetalleTerceroComponent } from './detalle-tercero/detalle-tercero.component';


@NgModule({
  declarations: [DetalleTerceroComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    ModalModule.forRoot(),
  ],
  exports: [DetalleTerceroComponent]
})
export class IPredialCommonModule { }
