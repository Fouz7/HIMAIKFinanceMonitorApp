import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Transaction} from '../Interfaces/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private url = 'https://localhost:44394/Transaction/GetAllTransactions';
  constructor(private http: HttpClient) { }

  getAllTransaction(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.url);
  }
}
