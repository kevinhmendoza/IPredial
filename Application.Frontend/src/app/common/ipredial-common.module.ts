import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClipboardModule } from 'ngx-clipboard';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DetalleTerceroComponent } from './detalle-tercero/detalle-tercero.component';
import { DataTableGenericComponent } from './data-table-generic/data-table-generic.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';


@NgModule({
  declarations: [DetalleTerceroComponent, DataTableGenericComponent],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    Ng2SmartTableModule,
    ModalModule.forRoot(),
  ],
  exports: [DetalleTerceroComponent, DataTableGenericComponent]
})
export class IPredialCommonModule { }
