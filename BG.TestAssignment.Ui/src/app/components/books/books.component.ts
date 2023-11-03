import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Book } from 'src/app/models/book';
import { BooksService } from 'src/app/services/books.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit{

  Books: Book[] = [];
  editedBook: Book | null = null;

  constructor(
    private router: Router,
    private bookService : BooksService
    ){

  }

  ngOnInit(): void {
    this.getAllBooks();
  }

  getAllBooks(){
    this.bookService.getAllBooks()
      .subscribe(us=>{
        this.Books = us;
      })
  }

  addBook() {
    this.router.navigate(['add-book']);
  }


  editBook(id : number, book: Book) {
    this.editedBook = new Book(book.id ,book.title, book.publishedDate, book.bookGenre, book.authorId );
    this.bookService.updateBook(id,this.editedBook);
    // this.refresh();
  }

  deleteBook(id: number){
    if(this.bookService.deleteBook(id))
    this.Books.splice(id,1);
    
    // this.refresh();
  }

  refresh(){
    window.location.reload();
  }
}
