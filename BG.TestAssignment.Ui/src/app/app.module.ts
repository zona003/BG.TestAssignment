import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {
  HTTP_INTERCEPTORS,
  HttpClient,
  HttpClientModule,
} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { BooksComponent } from './components/books/books.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { AUTH_API_URL, BOOK_API_URL } from './app-injection-tokens';
import { environment } from './environments/environment';
import { ACCESS_TOKEN_KEY } from './services/auth.service';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { JwtInterceptor } from './_helpers/jwtinterceptor';
import { AuhtorFormComponent } from './components/auhtor-form/auhtor-form.component';
import { BookFormComponent } from './components/book-form/book-form.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { DialogModule } from 'primeng/dialog';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    AuthorsComponent,
    UserComponent,
    LoginComponent,
    RegisterComponent,
    AuhtorFormComponent,
    BookFormComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    TableModule,
    CardModule,
    ButtonModule,
    PasswordModule,
    InputTextModule,
    CalendarModule,
    DialogModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenWhitelistedDomains,
      },
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: AUTH_API_URL,
      useValue: environment.monoApi,
    },
    {
      provide: BOOK_API_URL,
      useValue: environment.monoApi,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
