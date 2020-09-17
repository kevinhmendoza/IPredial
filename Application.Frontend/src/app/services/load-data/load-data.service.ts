import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Summary } from 'src/app/entities/summary';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadDataService {

  constructor(private _httpService: HttpClient) { }

  public GetData(): Observable<Summary> {
    const observableResponse = new Observable<Summary>(observer => {
      if (!HelperSummaryService.Summary) {
        this._httpService.get<Summary>('./config/data.json').subscribe(datos => {
          HelperSummaryService.Summary = datos;
          observer.next(HelperSummaryService.Summary);
        });
      } else {
        observer.next(HelperSummaryService.Summary);
      }
    });
    return observableResponse;
  }
}

export class HelperSummaryService {
  public static Summary: Summary;
}