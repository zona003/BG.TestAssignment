import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/models/book';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit{

  Books: Book[] = []; 

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
