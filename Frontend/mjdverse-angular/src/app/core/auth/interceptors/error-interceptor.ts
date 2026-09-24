import {
  HttpErrorResponse,
  HttpInterceptorFn
} from '@angular/common/http';

import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      console.error('HTTP Error:', {
        status: error.status,
        message: error.message,
        url: error.url
      });

      return throwError(() => error);
    })
  );
};