import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { Book } from "src/app/models/book";
import { BooksService } from "src/app/services/books.service";

@Component({
    selector: "app-book-form",
    templateUrl: "./book-form.component.html",
    styleUrls: ["./book-form.component.scss"],
})
export class BookFormComponent implements OnChanges {
    @Input() dispalyAddModal: boolean = true;
    @Input() editedBook: Book | null = null;
    @Output() clickClose: EventEmitter<boolean> = new EventEmitter<boolean>();
    modalType = "Add";

    currentBook: Book | null = null;
    editForm: FormGroup;
    edit: boolean = false;

    subscriptions: Array<Subscription> = new Array();
    constructor(private fb: FormBuilder, private bookService: BooksService, private router: Router) {
        this.editForm = this.fb.group({
            title: ["", Validators.required],
            publishedDate: ["", Validators.required],
            bookGenre: ["", Validators.minLength(3)],
            authorId: [Number, Validators.required],
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
        this.clickClose.emit(true);
    }

    addEditBook() {
        const bookData = this.editForm.value;
        if (this.editForm.valid) {
            this.currentBook = new Book(0, bookData.title, bookData.publishedDate, bookData.bookGenre, bookData.authorId);
            if (this.editedBook == null) {
                var sub = this.bookService.createBook(this.currentBook).subscribe((respon) => {
                    if (respon.errors == null) {
                        this.editForm.reset();
                        this.closeModal();
                        this.refresh();
                    }
                });
                this.subscriptions.push(sub);
            } else {
                this.currentBook.id = this.editedBook.id;
                var sub = this.bookService.updateBook(this.editedBook.id, this.currentBook).subscribe((respon) => {
                    if (respon.errors == null) {
                        this.editForm.reset();
                        this.closeModal();
                        this.refresh();
                    }
                });
                this.subscriptions.push(sub);
                this.editedBook = null;
            }
        }
    }

    refresh() {
        //window.location.reload();
    }
    cancell() {
        this.subscriptions.forEach((element) => {
            element.unsubscribe();
        });
        this.closeModal();
    }
}
