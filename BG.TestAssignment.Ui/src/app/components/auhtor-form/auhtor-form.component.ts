import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Author } from 'src/app/models/author';
import { AuthorsService } from 'src/app/services/authors.service';

@Component({
  selector: 'app-auhtor-form',
  templateUrl: './auhtor-form.component.html',
  styleUrls: ['./auhtor-form.component.scss'],
})
export class AuhtorFormComponent implements OnChanges {
  @Input() dispalyAddModal: boolean = true;
  @Input() editedAuthor: Author | null = null;
  @Output() clickClose: EventEmitter<boolean> = new EventEmitter<boolean>();
  modalType = 'Add';

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
  ngOnChanges(changes: SimpleChanges): void {
    if (this.editedAuthor) {
      this.modalType = 'Edit';
      this.editForm.patchValue(this.editedAuthor);
    } else {
      this.modalType = 'Add';
      this.editForm.reset();
    }
  }

  closeModal() {
    this.clickClose.emit(true);
  }

  addEditAuthor() {
    const authorData = this.editForm.value;
    if (this.editForm.valid) {
      if (this.editedAuthor == null) {
        this.currentAuthor = new Author(
          0,
          authorData.firstName,
          authorData.lastName,
          authorData.birthDate
        );
        this.authorService.createAuthor(this.currentAuthor);
      } else {
        this.currentAuthor = new Author(
          this.editedAuthor.id,
          authorData.firstName,
          authorData.lastName,
          authorData.birthDate
        );
        this.authorService.updateAuthor(
          this.editedAuthor.id,
          this.currentAuthor
        );
        this.editedAuthor = null;
      }
    }

    this.editForm.reset();
    this.closeModal();
    this.refresh();
  }
  refresh() {
    window.location.reload();
  }
}
