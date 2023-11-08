import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
    selector: "app-login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.scss"],
})
export class LoginComponent {
    form: FormGroup;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
    ) {
        this.form = this.fb.group({
            username: ["", Validators.required],
            password: ["", Validators.required],
        });
    }

    login() {
        const val = this.form.value;

        if (val.username && val.password) {
            this.authService.login(val.username, val.password).subscribe({
                next: (v) => this.router.navigateByUrl("/"),
                error: (e) => alert("Wrong credentials"),
                complete: () => this.router.navigateByUrl("/"),
            });
        }
    }

    register() {
        this.router.navigateByUrl("/register");
    }

    setDirty() {}
}
