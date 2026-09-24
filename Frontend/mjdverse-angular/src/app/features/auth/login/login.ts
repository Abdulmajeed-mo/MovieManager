import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { AuthService } from '../../../core/auth/services/auth';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  readonly loginForm;
  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {
    this.loginForm = this.formBuilder.nonNullable.group({
      identifier: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  login(): void {
    this.errorMessage = '';

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const request = this.loginForm.getRawValue();

    this.authService.login(request).subscribe({
      next: () => {
        this.router.navigate(['/verify-login-otp'], {
          queryParams: {
            email: request.identifier
          }
        });
      },

      error: (error) => {
        console.error('Login failed:', error);

        this.errorMessage =
          'Login failed. Please check your email and password.';
      }
    });
  }
}