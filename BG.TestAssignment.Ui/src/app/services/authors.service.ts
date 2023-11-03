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
    @Inject(BOOK_API_URL) private apiUrl: string,
  ) { }

  private baseApiUrl = `${this.apiUrl}/api/lib/Authors`

  getAllAuthor(): Observable<Author[]> {
    return this.http.get<Author[]>(`${this.baseApiUrl}`);
  }

  getAuthor(id: number): Observable<Author> {
    return this.http.get<Author>(`${this.baseApiUrl}/${id}`);
  }

  createAuthor(book : Author) : boolean{
    this.http.post<Author>(`${this.baseApiUrl}`, {book})
    .subscribe(ansver=>{
      return ansver;
    })
    return false;
  }

  updateAuthor(id: number, book :Author): boolean{
    this.http.put<Author>(`${this.baseApiUrl}/${id}`, { book})
    .subscribe(ansver=>{
      return ansver;
    })
    return false;
  }

  deleteAuthor(id:number) : boolean{
    this.http.delete<Author>(`${this.baseApiUrl}/${id}`)
    .subscribe(ansver=>{
      return ansver;
    })
    return false;
  }
}
