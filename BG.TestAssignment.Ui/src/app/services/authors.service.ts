import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BOOK_API_URL } from '../app-injection-tokens';
import { Observable } from 'rxjs';
import { Author } from '../models/author';

@Injectable({
  providedIn: 'root'
})
export class AuthorsService {
  constructor(
    private http: HttpClient,
    @Inject(BOOK_API_URL) private booksApi: string,
  ) { }

  private baseApiUrl = `${this.booksApi}/api/Books`

  getAuthors() {
    return this.http.get<Array<Author>>(this.baseApiUrl);
  }

  getAuthor(id: number) {
    return this.http.get<Author>(this.baseApiUrl+'/'+ id);
  }

  createAuthor(user: Author) {
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    return this.http.post<Author>(this.baseApiUrl, JSON.stringify(user), { headers: myHeaders });
  }
  updateAuthor(user: Author) {
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    return this.http.put<Author>(this.baseApiUrl, JSON.stringify(user), { headers: myHeaders });
  }
  deleteAuthor(id: number) {

    return this.http.delete<Author>(this.baseApiUrl + '/' + id);
  }
}
