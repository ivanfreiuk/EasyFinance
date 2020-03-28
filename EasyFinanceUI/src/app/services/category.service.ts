import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  
  private apiUrl: string = 'https://localhost:44398/api';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}/categories`)
  }

  getById(id: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/categories/${id}`);
  }

  post(category: Category) {
    return this.http.post(`${this.apiUrl}/categories`, category);
  }

  update(category: Category) {
    return this.http.put(`${this.apiUrl}/categories/${category.id}`, category);
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/categories/${id}`);
  }
}
