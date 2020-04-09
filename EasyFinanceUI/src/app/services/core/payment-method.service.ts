import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaymentMethod } from '../../models/payment-method';

@Injectable({
  providedIn: 'root'
})
export class PaymentMethodService {
  
  private apiUrl: string = 'https://localhost:44398/api';

  constructor(private http: HttpClient) { }

  getAll(): Observable<PaymentMethod[]> {
    return this.http.get<PaymentMethod[]>(`${this.apiUrl}/paymentmethods`)
  }

  getById(id: number): Observable<PaymentMethod> {
    return this.http.get<PaymentMethod>(`${this.apiUrl}/paymentmethods/${id}`);
  }

  post(paymentMethod: PaymentMethod) {
    return this.http.post(`${this.apiUrl}/paymentmethods`, paymentMethod);
  }

  update(paymentMethod: PaymentMethod) {
    return this.http.put(`${this.apiUrl}/paymentmethods/${paymentMethod.id}`, paymentMethod);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/paymentmethods/${id}`);
  }
}
