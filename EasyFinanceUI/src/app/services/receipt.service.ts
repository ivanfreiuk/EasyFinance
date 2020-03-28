import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Receipt } from '../models/receipt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {

  private apiUrl: string = 'https://localhost:44398/api';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Receipt[]> {
    return this.http.get<Receipt[]>(`${this.apiUrl}/receipts`)
  }

  getById(id: number): Observable<Receipt> {
    return this.http.get<Receipt>(`${this.apiUrl}/receipts/${id}`);
  }

  post(receipt: Receipt) {
    return this.http.post(`${this.apiUrl}/receipts/create`, receipt);
  }

  update(receipt: Receipt) {
    return this.http.put(`${this.apiUrl}/receipts/${receipt.id}`, receipt);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/receipts/${id}`);
  }
}
