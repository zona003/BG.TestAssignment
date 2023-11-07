import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from 'src/app/models/author';
import { ResponceWrapper } from 'src/app/models/responceWrapper';
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
    this.getAllAuthors();
  }

  private getAllAuthors() {
    this.serv.getAllAuthor().subscribe((us :ResponceWrapper<Author[]>) => {
      this.Authors = us.data;
    });
  }

  deleteAuthor(id: number) {
    this.serv.deleteAuthor(id).subscribe(a=>{
      this.Authors.splice(id,1);
    });
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
    //window.location.reload();
    this.getAllAuthors();
  }
}
