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
      console.log(this.totalRecords);
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
    this.getAllAuthors(this.page * this.rows, this.rows);
  }

  onPageChange(event: any) {
    this.page = event.first || 0;
    this.rows = event.rows || 10;
    console.log(this.page + " page "+ this.rows+" rows")

    this.serv.getAllAuthor(this.page , this.rows).subscribe((us) => {
      console.warn("huita");
      this.Authors = us.data.items;
      this.totalRecords = us.data.total;
    });
  }
}
