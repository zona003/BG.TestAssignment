import { Component } from "@angular/core";
import { AuthService } from "./services/auth.service";
import { Router } from "@angular/router";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.scss"],
})
export class AppComponent {
    constructor(
        private as: AuthService,
        private router: Router,
    ) {}

    public userFirstName: string = "";

    public get isLoggedIn(): boolean {
        return this.as.isAutenticated();
    }

    login(userName: string, password: string) {
        this.as.login(userName, password).subscribe({
            next: (e) => (this.userFirstName = this.as.getFirstName()),
            error: (e) => alert("Wrong login or password."),
        });
    }

    logout() {
        this.as.logout();
        this.router.navigateByUrl("login");
    }
}
