import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse, HttpStatusCode
} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {Router} from "@angular/router";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
        .pipe(
            catchError(((error: HttpErrorResponse) => {
              if (error.status === HttpStatusCode.InternalServerError || error.status === 0) {
                this.handleInternalSeverError()
              }
              return throwError(() => error)
            }))
        )
  }

  handleInternalSeverError() {
    this.router.navigate(['/internal-server-error']);
  }
}
