import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Currency } from '../../models/currency';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  
  private apiUrl: string = 'https://localhost:44398/api';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Currency[]> {
    return this.http.get<Currency[]>(`${this.apiUrl}/currencies`)
  }

  getById(id: number): Observable<Currency> {
    return this.http.get<Currency>(`${this.apiUrl}/currencies/${id}`);
  }

  post(currency: Currency) {
    return this.http.post(`${this.apiUrl}/currencies`, currency);
  }

  update(currency: Currency) {
    return this.http.put(`${this.apiUrl}/currencies/${currency.id}`, currency);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/currencies/${id}`);
  }
}
