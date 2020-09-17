import { Injectable } from '@angular/core';
import { LoadDataService } from '../load-data/load-data.service';
import { Summary } from 'src/app/entities/summary';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SummaryResumeService {

  constructor(private _dataService: LoadDataService) { }

  public GetProfile(): Observable<Summary> {
    return this._dataService.GetData();
  }
}
