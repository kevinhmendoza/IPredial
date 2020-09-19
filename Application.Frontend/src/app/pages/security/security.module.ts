import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GestionUsuariosComponent } from './gestion-usuarios/gestion-usuarios.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClipboardModule } from 'ngx-clipboard';
import { ModalModule } from 'ngx-bootstrap/modal';


@NgModule({
  declarations: [GestionUsuariosComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    ModalModule.forRoot(),
  ]
})
export class SecurityModule { }
