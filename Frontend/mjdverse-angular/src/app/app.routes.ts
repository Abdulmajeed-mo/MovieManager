import { Routes } from '@angular/router';

import { MainLayout } from './layouts/main-layout/main-layout';
import { Home } from './features/home/home';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { VerifyOtp } from './features/auth/verify-otp/verify-otp';
import { VerifyLoginOtp } from './features/auth/verify-login-otp/verify-login-otp';
import { NotFound } from './pages/not-found/not-found';
import { guestGuard } from './core/auth/guards/guest-guard';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
      // Default route → Home
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      },

      // Home page
      {
        path: 'home',
        component: Home
      },

      // Login page
      {
        path: 'login',
        canActivate: [guestGuard],
        component: Login
      },

      // Register page
      {
        path: 'register',
        canActivate: [guestGuard],
        component: Register
      },

      // Verify registration email OTP
      {
        path: 'verify-otp',
        canActivate: [guestGuard],
        component: VerifyOtp
      },

      // Verify login OTP
      {
        path: 'verify-login-otp',
        canActivate: [guestGuard],
        component: VerifyLoginOtp
      },

      // 404 - Must be last
      {
        path: '**',
        component: NotFound
      }
    ]
  }
];