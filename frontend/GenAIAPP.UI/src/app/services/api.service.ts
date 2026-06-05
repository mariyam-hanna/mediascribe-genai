import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'http://localhost:5038/api/upload';

  constructor(private http: HttpClient) {}

  testConnection() {
    return this.http.get<any>(`${this.apiUrl}/test`);
  }
}