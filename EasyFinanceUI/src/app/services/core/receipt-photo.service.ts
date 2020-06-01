import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReceiptPhoto } from '../../models/receipt-photo';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReceiptPhotoService {

  private apiUrl: string = `${environment.apiUrl}/api`; //'https://localhost:44398/api'

  constructor(private http: HttpClient) { }

  getAll(): Observable<ReceiptPhoto[]> {
    return this.http.get<ReceiptPhoto[]>(`${this.apiUrl}/receiptphotos`)
  }

  getById(id: number): Observable<ReceiptPhoto> {
    return this.http.get<ReceiptPhoto>(`${this.apiUrl}/receiptphotos/${id}`);
  }

  getPhoto(id:number) {
    return this.http.get(`${this.apiUrl}/receiptphotos/blob/${id}`, { responseType: 'blob'});
  }

  post(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post(`${this.apiUrl}/receiptphotos`, formData);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/receiptphotos/${id}`);
  }
}
