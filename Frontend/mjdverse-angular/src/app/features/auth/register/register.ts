import { Component } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../../core/auth/services/auth';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register {
  readonly registerForm;

  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {
    this.registerForm = this.formBuilder.nonNullable.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', [Validators.required, Validators.minLength(3)]],
      phoneNumber: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    });
  }

  register(): void {
    this.errorMessage = '';

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const request = this.registerForm.getRawValue();
 console.log('Register request:', request);
    if (request.password !== request.confirmPassword) {
      this.errorMessage = 'Passwords do not match.';
      return;
    }

    this.authService.register(request).subscribe({
      next: () => {
        this.router.navigate(['/verify-otp'], {
          queryParams: {
            email: request.email
          }
        });
      },
      error: (error) => {
        console.error('Registration failed:', error);

        this.errorMessage =
          'Registration failed. Please check your information and try again.';
      }
    });
  }
}