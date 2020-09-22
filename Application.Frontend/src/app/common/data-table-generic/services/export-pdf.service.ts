import { Injectable } from '@angular/core';
import * as jsPDF from 'jspdf';
import 'jspdf-autotable';
import { ConvertColumnsAndRowsToArraysService } from './convert-columns-rows-to-array.service';

@Injectable()
export class ExportPdfService {

  constructor(private _convertColumnsAndRowsToArraysService: ConvertColumnsAndRowsToArraysService) {
  }

  private columns: Array<any>;
  private _Filas: Array<any>;
  private base64Img: string;
  private _NameReport: string;
  private _nameFileWithotExtension: string;


  /**
   * Exporta a pdf una lista de cualquier tipo
   * @param columns columnas de la tabla pdf
   * @param filas registros cuyos keys corresponden a las columnas
   * @param base64LogoAppImg imagen en base64 del logo de la aplicación
   * @param nameReport nombre del reporte
   * @param nameFileWithotExtension nombre con el cual se descargara el archivo
   */
  public exportPdf(columns: Array<any>, filas: Array<any>, base64LogoAppImg: string, nameReport: string, nameFileWithotExtension: string): void {
    this.columns = columns;
    this._Filas = filas;
    this.base64Img = base64LogoAppImg;
    this._nameFileWithotExtension = nameFileWithotExtension;
    this._NameReport = nameReport;
    //https://github.com/simonbengtsson/jsPDF-AutoTable ESTA ES LA DOCUMENTACIÓN
    const doc = new jsPDF('l', 'pt');

    let response = this._convertColumnsAndRowsToArraysService.Convert(columns, filas);
    let Columnas: Array<string> = response.Columnas;
    let Filas: Array<Array<string>> = response.Filas;

    var base64LogoAppImg = this.base64Img;
    var nombreInforme = this._NameReport;
    doc.autoTable({
      styles: { cellPadding: 1.5, fontSize: 8 },
      margin: { top: 35, left: 10, right: 10, bottom: 25 },
      head: [Columnas],
      body: Filas,
      didDrawPage: function (data) {
        doc.setFontSize(14);

        if (base64LogoAppImg) {
          doc.addImage(base64LogoAppImg, 'PNG', 10, 4, 28, 28);
        }

        doc.text(nombreInforme, data.settings.margin.left + 35, 22);

        doc.setFontSize(5);
        // Footer
        var str = "Página " + doc.internal.getNumberOfPages()

        var pageSize = doc.internal.pageSize;
        var pageHeight = pageSize.height ? pageSize.height : pageSize.getHeight();
        doc.text(str, data.settings.margin.left, pageHeight - 10);
      },
    });
    let nombreInformeSinExtension = this._nameFileWithotExtension;
    doc.save(nombreInformeSinExtension + '.pdf');
  }
}
