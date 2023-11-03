import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Author } from 'src/app/models/author';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss'],
  providers: [AuthorsService]
})
export class AuthorsComponent implements OnInit {

  editedAuthor: Author | null = null;
  Authors: Author[] = [];
  isNewRecord: boolean = false;
  statusMessage: string = "";

  constructor(
    private serv: AuthorsService,
    private router: Router
    ) {
    
  }

  ngOnInit(): void {
    this.loadAuthors();
  }


  private loadAuthors() {
    this.serv.getAllAuthor()
    .subscribe(us=>{
      this.Authors = us;
    })
  }

  addAuthor() {
    this.router.navigate(['add-author']);
  }


  editAuthor(id : number,author: Author) {
    this.editedAuthor = new Author(author.id ,author.firstName, author.lastName, author.birthDate );
    this.serv.updateAuthor(id,this.editedAuthor);
    // this.refresh();
  }

  deleteAuthor(id: number){
    this.serv.deleteAuthor(id);
    // this.refresh();
  }

  refresh(){
    window.location.reload();
  }
 
  // saveAuthor() {
  //   if (this.isNewRecord) {
      
  //     this.serv.createAuthor(this.editedAuthor as Author).subscribe(ans => {
  //       this.statusMessage = 'Данные успешно добавлены',
  //         this.loadAuthors();
  //     });
  //     this.isNewRecord = false;
  //     this.editedAuthor = null;
  //   } else {
      
  //     this.serv.updateAuthor(this.editedAuthor as Author).subscribe(_ => {
  //       this.statusMessage = 'Данные успешно обновлены',
  //         this.loadAuthors();
  //     });
  //     this.editedAuthor = null;
  //   }
  // }

  // cancel() {
    
  //   if (this.isNewRecord) {
  //     this.authors.pop();
  //     this.isNewRecord = false;
  //   }
  //   this.editedAuthor = null;
  // }

  // deleteAuthor(author: Author) {
  //   this.serv.deleteAuthor(author.id).subscribe(_ => {
  //     this.statusMessage = 'Данные успешно удалены',
  //       this.loadAuthors();
  //   });
  // }
}
