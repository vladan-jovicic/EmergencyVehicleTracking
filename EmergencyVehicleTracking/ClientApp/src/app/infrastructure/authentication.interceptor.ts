import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {catchError, Observable, throwError} from 'rxjs';
import {UserService} from "../services/user.service";
import {Router} from "@angular/router";

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor(private userService: UserService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(err => {
        if (err.status === 401) {
          // this.alertService.error('Error logging in.<br>Please check your credentials.', false, true);
          // this.router.navigateByUrl('/login');
          if (this.userService.currentUser) {
            this.userService.logout();
            return throwError(err);
          } else {
            this.router.navigateByUrl('/login');
            return throwError(err);
          }
        } else {
          return throwError(err);
        }
      })
    );
  }
}
