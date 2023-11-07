import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BOOK_API_URL } from '../app-injection-tokens';
import { Observable, map } from 'rxjs';
import { Author } from '../models/author';
import { ResponceWrapper } from '../models/responceWrapper';
import { PagedResponce } from '../models/padgedResponce';

@Injectable({
  providedIn: 'root',
})
export class AuthorsService {
  constructor(
    private http: HttpClient,
    @Inject(BOOK_API_URL) private apiUrl: string
  ) {}

  private baseApiUrl = `${this.apiUrl}/api/lib/Authors`;

  getAllAuthor(skip: number, take: number): Observable<ResponceWrapper<PagedResponce<Author[]>>> {
    return this.http.get<ResponceWrapper<PagedResponce<Author[]>>>(`${this.baseApiUrl}?skip=${skip}&take=${take}`);
  }

  getAuthor(id: number): Observable<ResponceWrapper<Author>> {
    return this.http.get<ResponceWrapper<Author>>(`${this.baseApiUrl}/${id}`);
  }

  createAuthor(auhtor: Author): Observable<ResponceWrapper<Author>> {
    return this.http
      .post<ResponceWrapper<Author>>(`${this.baseApiUrl}`, auhtor)
  }

  updateAuthor(id: number, auhtor: Author): Observable<ResponceWrapper<Author>> {
    return this.http
      .put<ResponceWrapper<Author>>(`${this.baseApiUrl}/${id}`, auhtor);
  }

  deleteAuthor(id: number): Observable<ResponceWrapper<Author>> {
    return this.http
      .delete<ResponceWrapper<Author>>(`${this.baseApiUrl}/${id}`)
  }
}
