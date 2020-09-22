import { Injectable } from '@angular/core';

@Injectable()
export class ConvertColumnsAndRowsToArraysService {

  constructor() { }

  /**
   * Convierte filas y columnas en formato array de string
   * @param columns columnas en formato { title: 'Estado', name: 'EstadoBadge', classNameRow: 'text-center', replaceNameInExportFor: 'Estado', hideInExport: true,filter:false },
   * @param filas filas de cualquier objeto
   */
  public Convert(columns: Array<any>, filas: Array<any>): ConvertColumnsAndRowsToArraysResponse {
    let response = new ConvertColumnsAndRowsToArraysResponse();
    let Columnas: Array<string> = ["N°"];
    let ColumnasNames: Array<string> = [];
    let Filas: Array<Array<string>> = [];
    columns.forEach((column: any, indexfila: number) => {
      if (column.title) {
        if (!column.hideInExport) {//Si no desean ocultar la columna
          Columnas.push(column.title);
          if (column.replaceNameInExportFor) {//Si desean cambiar el valor de una columna por otra, esto es generalmente para cuando se visualiza un boton o cualquier otro valor que sea visual mas no un dato
            ColumnasNames.push(column.replaceNameInExportFor);
          } else {
            ColumnasNames.push(column.name);
          }
        }
      }
    });
    filas.forEach((fila: any, indexfila: number) => {
      Filas[indexfila] = [(indexfila + 1) + ""];
      ColumnasNames.forEach((column: any, indexcolumna: number) => {
        Filas[indexfila].push(fila[column]);
      });
    });
    response.Columnas = Columnas;
    response.ColumnasNames = ColumnasNames;
    response.Filas = Filas;
    return response;
  }
}

export class ConvertColumnsAndRowsToArraysResponse {
  public Columnas: Array<string>;
  public Filas: Array<Array<string>>;
  public ColumnasNames: Array<string>;
}
