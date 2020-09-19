import { Component, Input, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-confirmation-dialog',
    templateUrl: './confirmation-dialog.component.html',
})
//https://stackoverflow.com/questions/46408428/ngx-bootstrap-modal-how-to-get-a-return-value-from-a-modal
export class ConfirmationDialogComponent implements OnInit {

    title: string;
    message: string;
    btnOkText: string;
    btnCancelText: string;

    result: Subject<boolean> = new Subject<boolean>();

    constructor(public modalRef: BsModalRef) { }

    ngOnInit() {
    }

    public decline() {
        this.result.next(false);
        this.modalRef.hide();
    }

    public accept() {
        this.result.next(true);
        this.modalRef.hide();
    }

    public dismiss() {
        this.result.next(false);
        this.modalRef.hide();
    }

}
