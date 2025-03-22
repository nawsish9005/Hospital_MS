import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HmsService {
  private baseUrl = 'https://localhost:7212/api';
  constructor(private http: HttpClient) {}

  getAll<T>(controller: string): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${controller}`);
  }

  getById<T>(controller: string, id: number): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${controller}/${id}`);
  }

  create<T>(controller: string, data: T): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${controller}`, data);
  }

  update<T>(controller: string, id: number, data: T): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${controller}/${id}`, data);
  }

  delete(controller: string, id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${controller}/${id}`);
  }
}
