import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Book } from 'src/app/models/book';
import { BooksService } from 'src/app/services/books.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss'],
})
export class BooksComponent implements OnInit {
  Books: Book[] = [];
  editedBook: Book | null = null;
  dispalyAddModal: boolean = false;
  editForm: FormGroup;
  

  constructor(
    private router: Router,
    private bookService: BooksService,
    private fb: FormBuilder
  ) {
    this.editForm = this.fb.group({
      title: ['', Validators.required],
      publishedDate: ['', Validators.required],
      bookGenre: [null, Validators.required],
      authorId: [Number, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getAllBooks();
  }

  getAllBooks() {
    this.bookService.getAllBooks().subscribe((us) => {
      this.Books = us.data;
    });
  }


  // editBook(id: number, book: Book) {
  //   this.editedBook = new Book(
  //     book.id,
  //     book.title,
  //     book.publishedDate,
  //     book.bookGenre,
  //     book.authorId
  //   );
  //   this.bookService.updateBook(id, this.editedBook).subscribe(n => 
  //     {
  //       console.log(n.errors);

  //     });

  // }

  deleteBook(id: number) {
  this.bookService.deleteBook(id).subscribe(ans=>{
    this.Books.splice(id, 1);
  });
      
    
  }

  showAddModal() {
    this.dispalyAddModal = true;
    this.editedBook = null;
  }

  hideAddModal(isClosed: boolean) {
    this.dispalyAddModal = !isClosed;
  }

  showEditModal(book: Book) {
    this.dispalyAddModal = true;
    this.editedBook = book;
  }

  refresh() {
    window.location.reload();
  }
}
