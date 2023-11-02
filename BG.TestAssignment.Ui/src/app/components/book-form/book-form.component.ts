import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Book } from 'src/app/models/book';
import { BooksService } from 'src/app/services/books.service';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss']
})
export class BookFormComponent {
  currentBook: Book | null = null;
  editForm: FormGroup;
  edit: boolean = false;

  constructor(
    private fb: FormBuilder,
    private bookService: BooksService,
    private router: Router
  ) {
    this.editForm = this.fb.group({
      title: ['', Validators.required],
      publishedDate: ['', Validators.required],
      bookGenre: [null, Validators.required],
      authorId:[Number,Validators.required]
    });
  }


  onSubmit() {
    if (this.editForm.valid) {

          const bookData = this.editForm.value;
          var newBook = new Book(0, bookData.title, bookData.publishedDate, bookData.bookGenre, bookData.authorId);
          var result = this.bookService.createBook(newBook);
          console.log("add author result - " + result);
    }
  }

  cancell(){
    this.router.navigate(['books']);
  }
}
