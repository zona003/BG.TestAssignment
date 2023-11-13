import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { Author } from "src/app/models/author";
import { Book } from "src/app/models/book";
import { AuthorsService } from "src/app/services/authors.service";
import { BooksService } from "src/app/services/books.service";

@Component({
    selector: "app-book-form",
    templateUrl: "./book-form.component.html",
    styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent implements OnChanges, OnInit {
    @Input() dispalyAddModal: boolean = true;
    @Input() editedBook: Book | null = null;
    @Output() clickClose: EventEmitter<boolean> = new EventEmitter<boolean>();

    modalType = "Add";

    currentBook: Book | null = null;
    editForm: FormGroup;
    edit: boolean = false;

    takenAuthors : Author[] | undefined = [];
    allAuthors: Author [] = [];

    subscriptions: Array<Subscription> = new Array();

    constructor(private fb: FormBuilder, private bookService: BooksService, private router: Router, private authorService: AuthorsService) {
        this.editForm = this.fb.group({
            title: ["", Validators.required],
            publishedDate: ["", Validators.required],
            bookGenre: ["", Validators.minLength(3)],
            authors: [this.editedBook?.authorsInBooks]
        });
        this.takenAuthors = this.editedBook?.authorsInBooks;
    }
    ngOnInit(): void {
        this.authorService.getAllAuthor(null, null).subscribe((us)=>{
            this.allAuthors = us.data.items;
        });
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (this.editedBook) {
            this.modalType = "Edit";
            this.editForm.patchValue(this.editedBook);
        } else {
            this.modalType = "Add";
            this.editForm.reset();
        }
    }

    closeModal() {
        this.editForm.reset();
        this.editedBook = null;
        this.clickClose.emit(true);
    }

    addEditBook() {
        const bookData = this.editForm.value;
        if (this.editForm.valid) {
            this.currentBook = new Book(0, bookData.title, bookData.publishedDate, bookData.bookGenre, bookData.authors);
            if (this.editedBook == null) {
                var sub = this.bookService.createBook(this.currentBook).subscribe((respon) => {
                    if (respon.errors == null) {
                        this.editForm.reset();
                        this.closeModal();
                    }
                });
                this.subscriptions.push(sub);
            } else {
                this.currentBook.id = this.editedBook.id;
                var sub = this.bookService.updateBook(this.editedBook.id, this.currentBook).subscribe((respon) => {
                    if (respon.errors == null) {
                        this.editForm.reset();
                        this.closeModal();
                    }
                });
                this.subscriptions.push(sub);
                
            }
        }
    }

    cancell() {
        this.subscriptions.forEach((element) => {
            element.unsubscribe();
        });
        this.closeModal();
    }
}
