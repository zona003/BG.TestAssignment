import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from 'src/app/models/author';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss'],
  providers: [AuthorsService],
})
export class AuthorsComponent implements OnInit {
  dispalyAddModal: boolean = false;

  editedAuthor: Author | null = null;
  Authors: Author[] = [];

  constructor(private serv: AuthorsService, private router: Router) {}

  ngOnInit(): void {
    this.loadAuthors();
  }

  private loadAuthors() {
    this.serv.getAllAuthor().subscribe((us) => {
      this.Authors = us;
    });
  }

  editAuthor(id: number, author: Author) {
    this.editedAuthor = new Author(
      author.id,
      author.firstName,
      author.lastName,
      author.birthDate
    );
    this.serv.updateAuthor(id, this.editedAuthor);
    // this.refresh();
  }

  deleteAuthor(id: number) {
    if (this.serv.deleteAuthor(id)) this.refresh();
  }

  showAddModal() {
    this.dispalyAddModal = true;
  }

  hideAddModal(isClosed: boolean) {
    this.dispalyAddModal = !isClosed;
  }

  showAddEditModal(author: Author) {
    this.dispalyAddModal = true;
    this.editedAuthor = author;
  }

  refresh() {
    window.location.reload();
  }
}
