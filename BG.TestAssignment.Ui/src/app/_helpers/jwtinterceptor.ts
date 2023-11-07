import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../environments/environment';
import { AuthService } from '../services/auth.service';
import { ResponceWrapper } from '../models/responceWrapper';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthService) { }

    intercept(request: HttpRequest<ResponceWrapper<any>>, next: HttpHandler): Observable<HttpEvent<ResponceWrapper<any>>> {
        // add auth header with jwt if user is logged in and request is to the api url
        const token = this.authenticationService.getToken();
        if (token!= null ) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`
                }
            });
        }

        return next.handle(request);
    }
}