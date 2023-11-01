import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Author } from 'src/app/models/author';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss'],
  providers: [AuthorsService]
})
export class AuthorsComponent implements OnInit {
  @ViewChild('readOnlyTemplate', { static: false }) readOnlyTemplate: TemplateRef<any> | null = null;
  @ViewChild('editTemplate', { static: false }) editTemplate: TemplateRef<any> | null = null;

  editedAuthor: Author | null = null;
  authors: Array<Author>;
  isNewRecord: boolean = false;
  statusMessage: string = "";

  constructor(private serv: AuthorsService) {
    this.authors = new Array<Author>();
  }

  ngOnInit() {
    this.loadAuthors();
  }


  private loadAuthors() {
    this.serv.getAuthors().subscribe((data: Array<Author>) => {
      this.authors = data;
    });
  }

  addAuthor() {
    this.editedAuthor = new Author(0,"","",new Date);
    this.authors.push(this.editedAuthor);
    this.isNewRecord = true;
  }


  editAuthor(author: Author) {
    this.editedAuthor = new Author(author.id ,author.firstName, author.lastName, author.birthDate );
  }
 
  loadTemplate(author: Author) {
    if (this.editedAuthor && this.editedAuthor.id === author.id) {
      return this.editTemplate;
    } else {
      return this.readOnlyTemplate;
    }
  }
 
  saveAuthor() {
    if (this.isNewRecord) {
      
      this.serv.createAuthor(this.editedAuthor as Author).subscribe(_ => {
        this.statusMessage = 'Данные успешно добавлены',
          this.loadAuthors();
      });
      this.isNewRecord = false;
      this.editedAuthor = null;
    } else {
      
      this.serv.updateAuthor(this.editedAuthor as Author).subscribe(_ => {
        this.statusMessage = 'Данные успешно обновлены',
          this.loadAuthors();
      });
      this.editedAuthor = null;
    }
  }

  cancel() {
    
    if (this.isNewRecord) {
      this.authors.pop();
      this.isNewRecord = false;
    }
    this.editedAuthor = null;
  }

  deleteAuthor(author: Author) {
    this.serv.deleteAuthor(author.id).subscribe(_ => {
      this.statusMessage = 'Данные успешно удалены',
        this.loadAuthors();
    });
  }
}
