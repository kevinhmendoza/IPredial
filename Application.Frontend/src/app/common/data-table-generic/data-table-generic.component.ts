import { Component, OnInit, Input, Output, ViewChild, EventEmitter, SimpleChanges, ElementRef } from '@angular/core';
import * as moment from 'moment';
import { DynamicHtmlCellTableComponent, DynamicHtmlCellTableData } from './components/dynamic-html-cell-table.component';
import { LocalDataSource } from 'ng2-smart-table';
import { ExportPdfService } from './services/export-pdf.service';
import { GetImageLogoService } from './services/get-image-logo.service';
import { ConvertColumnsAndRowsToArraysService } from './services/convert-columns-rows-to-array.service';
import { DatosEntidad } from './datos-entidad';

@Component({
  selector: 'app-data-table-generic',
  templateUrl: './data-table-generic.component.html',
  styleUrls: ['./data-table-generic.component.css'],
  providers: [ExportPdfService, GetImageLogoService, ConvertColumnsAndRowsToArraysService]
})
export class DataTableGenericComponent implements OnInit {

  private base64Img: string = "";

  public settings = {
    actions: false,
    noDataMessage: 'No se encontraron registros',
    attr: {
      class: 'table dataTable table-striped table-bordered ng2-smart-table-convertBootstrap3-byAnibal'
    },
    columns: {},
    pager: {
      display: true,
      perPage: 30
    }
  };

  private _columns: Array<any> = [];
  @Input()
  /**
    *
    * @param colums sss
    */
  set columns(colums: Array<any>) {
    if (colums) {
      this._columns = colums;
      this.MapColumnToNg2SmartTable();
    }
  }
  get columns(): Array<any> {
    return this._columns;
  }

  @Input() itemsPerPage: number = 30;
  @Input() ShowLoading: boolean = true;

  private _Filas: Array<any> = [];
  /**
   * Este es el objeto que ng2-smart-table puede renderizar
   * */
  public representativesSource: LocalDataSource;

  @Input()
  set Filas(name: Array<any>) {
    if (name) {
      this._Filas = name;
      this.representativesSource = new LocalDataSource();
      this.representativesSource.load(this.Filas);
    }
  }

  get Filas(): Array<any> {
    return this._Filas;
  }

  // Outputs (Events)
  @Output() public CellClicked: EventEmitter<CellClickedTableGeneric> = new EventEmitter();

  private _NameReport: string = "Informe automático";

  @Input()
  set NameReport(name: string) {
    if (name) {
      this._NameReport = name;
    }
  }

  get NameReport(): string {
    return this._NameReport;
  }

  private DatosEntidad: DatosEntidad;

  constructor(private _getImageLogoService: GetImageLogoService, private _exportPdfService: ExportPdfService) {
    this.DatosEntidad = new DatosEntidad()
  }

  ngOnInit() {
    this.GetImageLogo();
  }

  /**
   * Formato de entrada
   * { title: 'Estado', name: 'EstadoBadge', classNameRow: 'text-center', replaceNameInExportFor: 'Estado', hideInExport: true,filter:false },
   * */
  private MapColumnToNg2SmartTable(): any {
    let columnasNuevas = {};
    this.columns.forEach(column => {
      let columnSetting = new SettingColumnNg2SmartTable(column.title);
      columnSetting.classContent = column.classNameRow;
      columnSetting.classColumn = column.className;
      columnSetting.filter = false;
      if (!column.hideInExport || column.filter) {
        columnSetting.filter = true;
      }
      columnSetting.filter = !column.hideInExport;
      columnasNuevas[column.name] = this.GetColumnSetting(columnSetting);
    });
    this.settings.columns = columnasNuevas;
  }


  public onCellClick(data: any): any {
    this.CellClicked.emit({ Columna: data.column, Fila: data.row });
  }

  /**
   *Cambiar itemsp por pagina
   * */
  public CambiarItemspPorPagina(): void {
    this.representativesSource.setPaging(1, this.itemsPerPage);
    this.Refrescar();
  }

  /**
   *Actualiza la tabla es decir su renderizado
   * */
  public Refrescar(): void {
    this.representativesSource.refresh();
  }

  public GetPageActual(): number { return this.representativesSource?.getPaging()?.page ?? 0; }
  public GetPages(): number { return this.representativesSource?.getPaging()?.perPage ?? 0; }
  public GetTotal(): number { return this.representativesSource?.count() ?? 0; }

  

  private GetImageLogo() {
    this._getImageLogoService.GetImage().subscribe(base64 => {
      this.base64Img = base64;
    });
  }

  public GetColumnSetting(setting: SettingColumnNg2SmartTable): any {
    let object = this;
    return {
      title: setting.title,
      type: setting.type,
      filter: setting.filter,
      class: setting.classColumn,//Esto es para poner una clase a la columna th no a todas las celdas de la columna
      valuePrepareFunction: (value, row, cell): DynamicHtmlCellTableData => {
        // DATA FROM HERE GOES TO renderComponent
        return { Value: value, Row: row, Cell: cell };
      },
      renderComponent: DynamicHtmlCellTableComponent,
      onComponentInitFunction(instance: DynamicHtmlCellTableComponent) {
        instance.classDiv = setting.classContent;
        instance.eventEmitter.subscribe((data: DynamicHtmlCellTableData) => {
          object.CellClicked.emit({ Columna: data.Cell.column.id, Fila: data.Row });
        });
      }
    };
  }

  public ExportarPdf() {
    this._exportPdfService.exportPdf(this.columns, this._Filas, this.base64Img, this._NameReport, this.GetNombreInforme());
  }

  /**
   * Obtiene el nombre del informe sin la extensión
   */
  private GetNombreInforme() {
    var fechaActual = moment().format('YYYYMMDDHHmmss');
    let nombreInformeSinExtension = this._NameReport + " " + fechaActual;
    return nombreInformeSinExtension;
  }
}


export class CellClickedTableGeneric {
  public Columna: string;
  public Fila: any;
}


export class SettingColumnNg2SmartTable {
  constructor(title: string, filter: boolean = true, classContent: string = "", classColumn: string = "") {
    this.title = title;
    this.filter = filter;
    this.classContent = classContent;
    this.classColumn = classColumn;
    this.type = 'custom';
  }
  public title: string = "";
  public filter: boolean;
  public classContent: string = '';
  /**
   * Clase del encabezado, es decir del th 
   **/
  public classColumn: string = '';
  public type: 'custom' | 'html' | 'text';
}
