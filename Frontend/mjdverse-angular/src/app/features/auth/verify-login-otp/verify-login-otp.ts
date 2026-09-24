import { Component } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../../../core/auth/services/auth';

@Component({
  selector: 'app-verify-login-otp',
  imports: [ReactiveFormsModule],
  templateUrl: './verify-login-otp.html',
  styleUrl: './verify-login-otp.scss'
})
export class VerifyLoginOtp {
  readonly otpForm;

  errorMessage = '';

  private readonly email: string;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly authService: AuthService
  ) {
    this.email = this.route.snapshot.queryParamMap.get('email') ?? '';

    this.otpForm = this.formBuilder.nonNullable.group({
      otp: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(4)
        ]
      ]
    });
  }

  verifyOtp(): void {
    this.errorMessage = '';

    if (this.otpForm.invalid || !this.email) {
      this.otpForm.markAllAsTouched();
      return;
    }

    const request = {
      email: this.email,
      otp: this.otpForm.getRawValue().otp
    };

    this.authService.verifyLoginOtp(request).subscribe({
      next: (response) => {
        this.authService.setToken(response.token);
        this.router.navigate(['/movies']);
      },
      error: (error) => {
        console.error('OTP verification failed:', error);

        this.errorMessage =
          'Invalid or expired OTP. Please try again.';
      }
    });
  }
}