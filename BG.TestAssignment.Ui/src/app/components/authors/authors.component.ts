import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { TablePageEvent } from 'primeng/table';
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
  totalRecords: number = 20;
  rows : number = 10;
  page : number = 0;


  constructor(private serv: AuthorsService, private router: Router) {}

  ngOnInit(): void {
    this.getAllAuthors(0, this.rows);
  }

  private getAllAuthors(skip: number, take:number) {
    this.serv.getAllAuthor(skip, take).subscribe((us) => {
      this.totalRecords = us.data.total;
      this.Authors = us.data.items;
    });
  }

  deleteAuthor(id: number) {
    this.serv.deleteAuthor(id).subscribe(a=>{
      // this.Authors.splice(id,1);
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
    this.getAllAuthors(this.page , this.rows);
  }

  onPageChange(event: TablePageEvent) {
    this.page = event.first ;
    this.rows = event.rows ;

    this.serv.getAllAuthor(this.page , this.rows).subscribe((us) => {
      this.Authors = us.data.items;
      this.totalRecords = us.data.total;
    });
  }
}
