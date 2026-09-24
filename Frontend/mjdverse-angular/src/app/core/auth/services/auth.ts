import { Injectable } from '@angular/core';

import { LoginRequest } from '../../models/auth/login-request';
import { VerifyLoginOtpRequest } from '../../models/auth/verify-login-otp-request';

import { ApiService } from '../../services/api';
import { RegisterRequest } from '../../models/auth/register-request';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly tokenKey = 'mjdverse_token';

  constructor(private readonly api: ApiService) {}

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

  
register(request: RegisterRequest) {
  return this.api.postText('auth/register', request);
}


  //يتحقق مما إذا كان المستخدم قد تم تسجيل دخوله عن طريق التحقق من وجود الـ JWT في التخزين المحلي.
  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  

  login(request: LoginRequest) {
  return this.api.postText('auth/login', request);
}

  verifyLoginOtp(request: VerifyLoginOtpRequest) {
    return this.api.post<{ token: string }>(
      'auth/verify-login-otp',
      request
    );
  }
}