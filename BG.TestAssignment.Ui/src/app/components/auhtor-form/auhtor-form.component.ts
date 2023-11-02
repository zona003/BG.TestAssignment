import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Author } from 'src/app/models/author';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-auhtor-form',
  templateUrl: './auhtor-form.component.html',
  styleUrls: ['./auhtor-form.component.scss']
})
export class AuhtorFormComponent implements OnInit {
  currentAuthor: Author | null = null;
  editForm: FormGroup;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authorService: AuthorsService
  ) {
    this.editForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: [null, Validators.required],
    });
  }

  ngOnInit() {

  }

  onSubmit() {
    if (this.editForm.valid) {
      const authorData = this.editForm.value;
      var newAuthor = new Author(0, authorData.firstName, authorData.lastName, authorData.birthDate)
      var result = this.authorService.createAuthor(newAuthor);
      console.log("add author result - " + result);

    }
  }

  cancell(){
    this.router.navigate(['authors']);
  }
}
