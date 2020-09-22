import { Component, Input, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  selector: 'dynamic-html-compiler-anibal',
  template: `
    <div class="{{classDiv}}" [innerHTML]="value.Value" (click)="ClickCell()"></div>
  `,
})
export class DynamicHtmlCellTableComponent implements ViewCell {

  public classDiv: string = "";

  constructor() {
  }

  @Input() value: any;
  @Input() rowData: any;

  @Output() eventEmitter: EventEmitter<DynamicHtmlCellTableData> = new EventEmitter<DynamicHtmlCellTableData>();


  public ClickCell(): void {
    this.eventEmitter.emit(this.value);
  }
}

export class DynamicHtmlCellTableData {
  /**
   * Valor de la columna
   */
  public Value: any;
  /**
   * Fila completa
   */
  public Row: any;
  /**
   * Celda
   */
  public Cell: any;
}
