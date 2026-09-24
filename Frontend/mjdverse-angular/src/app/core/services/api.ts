import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly baseUrl = environment.apiUrl;

  constructor(private readonly http: HttpClient) {}

  get<T>(endpoint: string) {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}`);
  }

  post<T>(endpoint: string, body: unknown) {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body);
  }

  postText(endpoint: string, body: unknown) {
    return this.http.post(`${this.baseUrl}/${endpoint}`, body, {
      responseType: 'text'
    });
  }

  put<T>(endpoint: string, body: unknown) {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body);
  }

  delete<T>(endpoint: string) {
    return this.http.delete<T>(`${this.baseUrl}/${endpoint}`);
  }
}