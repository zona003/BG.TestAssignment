import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BOOK_API_URL } from '../app-injection-tokens';
import { Book } from '../models/book';


@Injectable({
  providedIn: 'root'
})
export class BooksService {

books : Book[] = []

  constructor(
    private http: HttpClient,
    @Inject(BOOK_API_URL) private apiUrl: string,
    ) {}
}
