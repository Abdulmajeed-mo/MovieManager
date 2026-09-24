import { Component } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ApiService } from '../../../core/services/api';

@Component({
  selector: 'app-verify-otp',
  imports: [ReactiveFormsModule],
  templateUrl: './verify-otp.html',
  styleUrl: './verify-otp.scss'
})
export class VerifyOtp {
  readonly otpForm;

  errorMessage = '';

  private readonly email: string;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly api: ApiService
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

this.api.postText('auth/verify-otp', request).subscribe({
        next: () => {
        this.router.navigate(['/login']);
      },
      error: (error) => {
        console.error('OTP verification failed:', error);

        this.errorMessage =
          'Invalid or expired OTP. Please try again.';
      }
    });
  }
}