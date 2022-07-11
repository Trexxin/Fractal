import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      // Catches the majority of error cases
      catchError(error => {
        if (error) {
          switch (error.status) {
            case 400:
              // Checks for an error response that has nested error objects
              if (error.error.errors) 
              {
                const modalStateErrors = [];
                // Loops over each key in the nested errors object
                for (const key in error.error.errors)
                {
                  if (error.error.errors[key]) 
                    // Flattans the array of errors from error responses and pushes them into the modlaStateErrors array
                    modalStateErrors.push(error.error.errors[key])
                    this.toastr.error(error.statusText === "OK" ? "Password must be between 4 and 8 charecters long" : error.statusText, error.status);
                }
                throw modalStateErrors.flat();
              } else {
                this.toastr.error(error.statusText === "OK" ? "Bad Request" : error.statusText, error.status);
              }
              break;
            case 401:
              this.toastr.error(error.statusText === "OK" ? "Unauthorized" : error.statusText, error.status);
              break;
            case 404:
              // Redirects user to a 404 not found page
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              // Redirects user to a server error page and collects the details of the error
              const navigationExtras: NavigationExtras = {state: {error: error.error}}
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error('Something unexpected happend');
              console.log(error);
              break;
          }
        }
        // Returns the error to what what causing the http request
        return throwError(error);
      })
    )
  }
}
