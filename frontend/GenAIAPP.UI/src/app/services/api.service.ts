import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'http://localhost:5038/api/upload';

  constructor(private http: HttpClient) {}

  uploadFile(formData: FormData) {
    return this.http.post<{ content: string }>(
      this.apiUrl,
      formData
    );
  }
}