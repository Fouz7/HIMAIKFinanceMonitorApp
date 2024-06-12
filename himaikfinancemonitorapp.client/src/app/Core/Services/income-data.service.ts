import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Income } from '../Interfaces/income';
import { IncomeDataDto } from '../Interfaces/income-data-dto';

@Injectable({
  providedIn: 'root'
})
export class IncomeDataService {
  private url = 'https://localhost:44394/IncomeData';

  constructor(private http: HttpClient) { }

  getAllIncomeData(nominalSortOrder: string, pageNumber: number, pageSize: number) {
    const params = new HttpParams()
      .set('nominalSortOrder', nominalSortOrder)
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<Income[]>(this.url + '/GetAllIncomeData', { params });
  }

  addIncomeData(incomeData: IncomeDataDto): Observable<IncomeDataDto> {
    return this.http.post<IncomeDataDto>(this.url + '/AddIncomeData', incomeData);
  }
}
