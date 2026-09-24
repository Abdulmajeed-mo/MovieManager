import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  private errorMessage: string | null = null;

  setError(message: string): void {
    this.errorMessage = message;
  }

  getError(): string | null {
    return this.errorMessage;
  }

  clearError(): void {
    this.errorMessage = null;
  }
}