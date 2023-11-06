import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Book } from 'src/app/models/book';
import { BooksService } from 'src/app/services/books.service';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss'],
})
export class BookFormComponent implements OnChanges {
  @Input() dispalyAddModal: boolean = true;
  @Input() editedBook: Book | null = null;
  @Output() clickClose: EventEmitter<boolean> = new EventEmitter<boolean>();
  modalType = 'Add';

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
      bookGenre: ['', Validators.minLength(3)],
      authorId: [Number, Validators.required],
    });
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (this.editedBook) {
      this.modalType = 'Edit';
      this.editForm.patchValue(this.editedBook);
    } else {
      this.modalType = 'Add';
      this.editForm.reset();
    }
  }

  closeModal() {
    this.clickClose.emit(true);
  }

  addEditBook() {
    const bookData = this.editForm.value;
    if (this.editForm.valid) {
      if (this.editedBook == null) {
        this.currentBook = new Book(
          0,
          bookData.title,
          bookData.publishedDate,
          bookData.bookGenre,
          bookData.authorId
        );
        this.bookService.createBook(this.currentBook);
      } else {
        this.currentBook = new Book(
          this.editedBook.id,
          bookData.title,
          bookData.publishedDate,
          bookData.bookGenre,
          bookData.authorId
        );
        this.bookService.updateBook(this.editedBook.id, this.currentBook);
        this.editedBook = null;
      }
    }

    this.editForm.reset();
    this.closeModal();
    this.refresh();
    // this.router.navigateByUrl('/');
  }

  refresh() {
    window.location.reload();
  }
}
