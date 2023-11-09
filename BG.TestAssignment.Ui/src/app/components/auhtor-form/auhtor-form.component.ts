import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { Author } from "src/app/models/author";
import { AuthorsService } from "src/app/services/authors.service";

@Component({
    selector: "app-auhtor-form",
    templateUrl: "./auhtor-form.component.html",
    styleUrls: ["./auhtor-form.component.scss"],
})
export class AuhtorFormComponent implements OnChanges {
    @Input() dispalyAddModal: boolean = true;
    @Input() editedAuthor: Author | null = null;
    @Output() clickClose: EventEmitter<boolean> = new EventEmitter<boolean>();
    modalType = "Add";

    currentAuthor: Author | null = null;
    editForm: FormGroup;

    subscriptions: Array<Subscription> = new Array();

    constructor(private router: Router, private fb: FormBuilder, private authorService: AuthorsService) {
        this.editForm = this.fb.group({
            firstName: ["", Validators.required],
            lastName: ["", Validators.required],
            birthDate: [null, Validators.required],
        });
    }
    ngOnChanges(changes: SimpleChanges): void {
        if (this.editedAuthor) {
            this.modalType = "Edit";
            this.editForm.patchValue(this.editedAuthor);
        } else {
            this.modalType = "Add";
            this.editForm.reset();
        }
    }

    closeModal() {
        this.clickClose.emit(true);
    }

    addEditAuthor() {
        const authorData = this.editForm.value;
        if (this.editForm.valid) {
            this.currentAuthor = new Author(0, authorData.firstName, authorData.lastName, authorData.birthDate);
            if (this.editedAuthor == null) {
                var sub = this.authorService.createAuthor(this.currentAuthor).subscribe((respon) => {
                    if (respon.errors == null) {
                        this.editForm.reset();
                        this.closeModal();
                    }
                });
                this.subscriptions.push(sub);
            } else {
                this.currentAuthor.id = this.editedAuthor.id;
                var sub = this.authorService.updateAuthor(this.editedAuthor.id, this.currentAuthor).subscribe((respon) => {
                    if (respon.errors == null) {
                        this.editForm.reset();
                        this.closeModal();
                    }
                });
                this.subscriptions.push(sub);
                this.editedAuthor = null;
            }
        }
    }

    cancell() {
        this.subscriptions.forEach((element) => {
            element.unsubscribe();
        });
        this.closeModal();
    }
}
