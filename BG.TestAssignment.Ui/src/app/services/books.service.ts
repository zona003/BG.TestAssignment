import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BOOK_API_URL } from '../app-injection-tokens';
import { Book } from '../models/book';
import { Observable } from 'rxjs';
import { ResponceWrapper } from '../models/responceWrapper';

@Injectable({
  providedIn: 'root',
})
export class BooksService {
  constructor(
    private http: HttpClient,
    @Inject(BOOK_API_URL) private apiUrl: string
  ) {}

  private baseApiUrl = `${this.apiUrl}/api/lib/Books`;

  getAllBooks(): Observable<ResponceWrapper<Book[]>> {
    return this.http.get<ResponceWrapper<Book[]>>(`${this.baseApiUrl}`);
  }

  getBook(id: number): Observable<ResponceWrapper<Book>> {
    return this.http.get<ResponceWrapper<Book>>(`${this.baseApiUrl}/${id}`);
  }

  createBook(book: Book): Observable<ResponceWrapper<Book>> {
    return this.http.post<ResponceWrapper<Book>>(`${this.baseApiUrl}`, book);
  }

  updateBook(id: number, book: Book): Observable<ResponceWrapper<Book>> {
    return this.http
      .put<ResponceWrapper<Book>>(`${this.baseApiUrl}/${id}`, book)
  }

  deleteBook(id: number): Observable<ResponceWrapper<Book>> {
    return this.http.delete<ResponceWrapper<Book>>(`${this.baseApiUrl}/${id}`);
  }
}
