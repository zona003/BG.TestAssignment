import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { AUTH_API_URL } from '../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Token } from '../models/token';

export const ACCESS_TOKEN_KEY = "access_token";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  login(userName: string, password: string): Observable<Token> {
    console.log(`${this.apiUrl}/api/Auth/login`);
    return new Observable((subscriber)=>{
      this.http.post<Token>(`${this.apiUrl}/api/Auth/login`, {
        userName, password
    }).subscribe(token=>{
      console.log(token.token);
      localStorage.setItem(ACCESS_TOKEN_KEY, token.token);
    });
  });
}

  isAutenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    if (token && !this.jwtHelper.isTokenExpired(token))
      return true;
    else
      return false;
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }
}
