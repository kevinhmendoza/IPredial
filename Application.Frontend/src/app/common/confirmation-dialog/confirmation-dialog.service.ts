import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ConfirmationDialogComponent } from './confirmation-dialog.component';
import { BsModalService } from 'ngx-bootstrap/modal';

@Injectable()
export class ConfirmationDialogService {

    constructor(private modalService: BsModalService) { }


    public confirm(
        title: string,
        message: string,
        btnOkText: string = 'Aceptar',
        btnCancelText: string = 'Cancelar',
        dialogSize: 'sm' | 'lg' = 'sm'): Observable<boolean> {

        const modal = this.modalService.show(ConfirmationDialogComponent);
        modal.content.message = message;
        modal.content.title = title;
        modal.content.btnOkText = btnOkText;
        modal.content.btnCancelText = btnCancelText;
        modal.content.dialogSize = dialogSize;

        return modal.content.result;

        //const modalRef = this.modalService.show(ConfirmationDialogComponent, { size: dialogSize });
        //modalRef.componentInstance.title = title;
        //modalRef.componentInstance.message = message;
        //modalRef.componentInstance.btnOkText = btnOkText;
        //modalRef.componentInstance.btnCancelText = btnCancelText;

        //return modalRef.result;
    }

}
