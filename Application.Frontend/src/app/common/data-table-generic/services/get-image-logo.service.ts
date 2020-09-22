import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class GetImageLogoService {

  constructor() { }

  fileType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
  fileExtension = '.xlsx';

  /**
   * Obtiene la imagen del logo de la aplicación en formato base64
   * */
  public GetImage(url: string = "/config/ipredial.png"): Observable<string> {

    const observableResponse = new Observable<string>(observer => {
      let img = new Image();
      img.src = url;
      if (!img.complete) {
        img.onload = () => {
          observer.next(this.getBase64Image(img));
        };
        img.onerror = (err) => {
          let image = "";
          observer.next(image);
        };
      } else {
        observer.next(this.getBase64Image(img));
      }
    });

    return observableResponse;
  }

  private getBase64Image(img: HTMLImageElement): string {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    var base64 = dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
    return base64;
  }

}
