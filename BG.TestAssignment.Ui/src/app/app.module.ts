import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BooksComponent } from './components/books/books.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { AUTH_API_URL, BOOK_API_URL } from './app-injection-tokens';
import { environment } from './environments/environment';
import { JwtModule } from '@auth0/angular-jwt';
import { ACCESS_TOKEN_KEY } from './services/auth.service';
import { UserComponent } from './components/user/user.component'; 
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register/register.component';
import { JwtInterceptor } from './_helpers/jwtinterceptor';
import { AuhtorFormComponent } from './components/auhtor-form/auhtor-form.component';
import { BookFormComponent } from './components/book-form/book-form.component';

export function tokenGetter(){
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
    BookFormComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenWhitelistedDomains
      }
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    {
      provide: AUTH_API_URL,
      useValue: environment.authApi
    },
    {
      provide: BOOK_API_URL,
      useValue: environment.booksApi
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
