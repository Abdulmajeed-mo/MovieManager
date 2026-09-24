import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly tokenKey = 'mjdverse_token';

//يجيب ال JWT 
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
// يحفظ الـ JWT عند تسجيل الدخول.
  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }
// يحذف الـ JWT عند تسجيل الخروج.
  clearToken(): void {
    localStorage.removeItem(this.tokenKey);
  }
//يتحقق مما إذا كان المستخدم قد تم تسجيل دخوله عن طريق التحقق من وجود الـ JWT في التخزين المحلي.
  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}