import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { TablePageEvent } from "primeng/table";
import { Author } from "src/app/models/author";
import { Book } from "src/app/models/book";
import { AuthorsService } from "src/app/services/authors.service";
import { BooksService } from "src/app/services/books.service";

@Component({
    selector: "app-books",
    templateUrl: "./books.component.html",
    styleUrls: ["./books.component.scss"],
})
export class BooksComponent implements OnInit {
    Books: Book[] = [];
    Authors: Author[] = [];
    editedBook: Book | null = null;
    dispalyAddModal: boolean = false;
    editForm: FormGroup;
    totalRecords: number = 20;
    rows : number = 10;
    page : number = 0;

    constructor(private router: Router, private bookService: BooksService, private fb: FormBuilder, private authorService: AuthorsService) {
        this.editForm = this.fb.group({
            title: ["", Validators.required],
            publishedDate: ["", Validators.required],
            bookGenre: [null, Validators.required],
            authorId: [Number, Validators.required],
        });
    }

    ngOnInit(): void {
        this.authorService.getAllAuthor(null, null).subscribe((us)=>{
            this.Authors = us.data.items;
        });
        this.getAllBooks(0, this.rows);
    }

    getAllBooks(skip: number, take: number) {
        this.bookService.getAllBooks(skip, take).subscribe((us) => {
            this.Books = us.data.items;
        });
    }

    deleteBook(id: number) {
        this.bookService.deleteBook(id).subscribe((ans) => {
            this.Books.splice(id, 1);
        });
    }

    showAddModal() {
        this.dispalyAddModal = true;
        this.editedBook = null;
    }

    hideAddModal(isClosed: boolean) {
        this.dispalyAddModal = !isClosed;
        this.refresh();
    }

    showEditModal(book: Book) {
        this.dispalyAddModal = true;
        this.editedBook = book;
    }

    refresh() {
        this.getAllBooks(this.page, this.rows);
    }

    onPageChange(event: TablePageEvent) {
        this.page = event.first ;
        this.rows = event.rows ;

        this.bookService.getAllBooks(this.page, this.rows).subscribe((us) => {
            this.Books = us.data.items;
            this.totalRecords = us.data.total;
        });
    }
}
