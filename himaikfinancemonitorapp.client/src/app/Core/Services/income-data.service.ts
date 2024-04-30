import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Income } from '../Interfaces/income';

@Injectable({
  providedIn: 'root'
})
export class IncomeDataService {
  private url = 'https://localhost:44394/IncomeData/GetAllIncomeData';

  constructor(private http: HttpClient) { }

  getAllIncomeData(nominalSortOrder: string, month: string, week: string): Observable<Income[]> {
    let params = new HttpParams()
      .set('nominalSortOrder', nominalSortOrder)
      .set('month', month)
      .set('week', week);

    return this.http.get<Income[]>(this.url, { params });
  }
}
