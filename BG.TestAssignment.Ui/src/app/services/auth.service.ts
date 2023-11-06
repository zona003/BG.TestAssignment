import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { AUTH_API_URL } from '../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Token } from '../models/token';
import { UserRegister } from '../models/userRegister';

export const ACCESS_TOKEN_KEY = 'access_token';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {}

  private firstName = '';

  login(userName: string, password: string): Observable<Token> {
    console.log(`${this.apiUrl}/api/Auth/login`);
    return new Observable((subscriber) => {
      this.http
        .post<Token>(`${this.apiUrl}/api/Auth/login`, {
          userName,
          password,
        })
        .subscribe((token) => {
          this.firstName = token.firstName;
          console.log(token);
          localStorage.setItem(ACCESS_TOKEN_KEY, token.token);
          this.router.navigateByUrl('/');
        });
    });
  }

  isAutenticated(): boolean {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    if (token && !this.jwtHelper.isTokenExpired(token)) return true;
    else return false;
  }

  getToken(): string {
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    if (token) return token;
    return '';
  }

  getFirstName(): string {
    return this.firstName;
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }

  registration(userRegister: UserRegister): boolean {
    this.http
      .post<UserRegister>(`${this.apiUrl}/api/Auth/register`, userRegister)
      .subscribe((ans) => {
        console.log(ans);
        this.router.navigate(['/login']);
        return true;
      });
    return false;
  }
}
