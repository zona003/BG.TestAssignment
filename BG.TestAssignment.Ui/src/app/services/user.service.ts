import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AUTH_API_URL } from '../app-injection-tokens';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl: string,
    ) {}

  private baseApiUrl = `${this.apiUrl}/api/User`
  
  getCurrentUser(): Observable<User>{
    return this.http.get<User>(`${this.baseApiUrl}`)
  }
}
