import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BOOK_API_URL } from '../app-injection-tokens';
import { Observable, map } from 'rxjs';
import { Author } from '../models/author';
import { ResponceWrapperAuthor } from '../models/responceWrapperAuthor';

@Injectable({
  providedIn: 'root',
})
export class AuthorsService {
  constructor(
    private http: HttpClient,
    @Inject(BOOK_API_URL) private apiUrl: string
  ) {}

  private baseApiUrl = `${this.apiUrl}/api/lib/Authors`;

  getAllAuthor(): Observable<ResponceWrapperAuthor> {
    return this.http.get<ResponceWrapperAuthor>(`${this.baseApiUrl}`);
  }

  getAuthor(id: number): Observable<ResponceWrapperAuthor> {
    return this.http.get<ResponceWrapperAuthor>(`${this.baseApiUrl}/${id}`);
  }

  createAuthor(auhtor: Author): ResponceWrapperAuthor {
    return this.http
      .post<ResponceWrapperAuthor>(`${this.baseApiUrl}`, auhtor)
      .subscribe((ansver) => {
        return ansver;
      });
  }

  updateAuthor(id: number, auhtor: Author): ResponceWrapperAuthor {
    return this.http
      .put<ResponceWrapperAuthor>(`${this.baseApiUrl}/${id}`, auhtor)
      .subscribe((ansver) => {
        return ansver;
      });
  }

  deleteAuthor(id: number): ResponceWrapperAuthor {
    return this.http
      .delete<ResponceWrapperAuthor>(`${this.baseApiUrl}/${id}`)
      .subscribe((ansver) => {
        return ansver;
      });
  }
}
