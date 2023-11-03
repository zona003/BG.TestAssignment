import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BOOK_API_URL } from '../app-injection-tokens';
import { Book } from '../models/book';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class BooksService {

  constructor(
    private http: HttpClient,
    @Inject(BOOK_API_URL) private apiUrl: string,
  ) { }

  private baseApiUrl = `${this.apiUrl}/api/lib/Books`

  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(`${this.baseApiUrl}`);
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<Book>(`${this.baseApiUrl}/${id}`);
  }

  createBook(book : Book) : boolean{
    this.http.post<Book>(`${this.baseApiUrl}`, {book})
    .subscribe(ansver=>{
      return ansver;
    })
    return false;
  }

  updateBook(id: number, book :Book): boolean{
    this.http.put<Book>(`${this.baseApiUrl}/${id}`, {id, book})
    .subscribe(ansver=>{
      return ansver;
    })
    return false;
  }

  deleteBook(id:number) : boolean{
    this.http.delete<Book>(`${this.baseApiUrl}/${id}`)
    .subscribe(ansver=>{
      return ansver;
    })
    return false;
  }
}
