import { Injectable } from "@angular/core";
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, every, tap } from "rxjs/operators";

import { AuthService } from "../../services/auth.service";
import { ResponceWrapper } from "src/app/models/responceWrapper";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private accountService: AuthService) {}

    intercept(request: HttpRequest<ResponceWrapper<any>>, next: HttpHandler): Observable<HttpEvent<ResponceWrapper<any>>> {
        if (request.body?.errors != null) {
            console.error(request.body.errors);
        }
        return next.handle(request).pipe(
            tap((event) => {
                if (event instanceof HttpResponse && event.body) {
                    const response: ResponceWrapper<any> = event.body;

                    if (response.errors && response.errors.length > 0) {
                        console.warn("Errors in response:", response.errors);
                    }
                }
            }),
            catchError((err) => {
                if ([401, 403].includes(err.status)) {
                    // auto logout if 401 or 403 response returned from api
                    this.accountService.logout();
                }
                const error = err.error?.message || err.statusText;
                console.error(err);
                return throwError(() => error);
            }),
        );
    }
}
