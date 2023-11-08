import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { BooksComponent } from "./components/books/books.component";
import { AuthorsComponent } from "./components/authors/authors.component";
import { UserComponent } from "./components/user/user.component";
import { authGuard } from "./common/guards/auth.guard";
import { LoginComponent } from "./components/login/login.component";
import { RegisterComponent } from "./components/register/register.component";
import { AuhtorFormComponent } from "./components/auhtor-form/auhtor-form.component";
import { BookFormComponent } from "./components/book-form/book-form.component";

const routes: Routes = [
    { path: "", component: UserComponent, canActivate: [authGuard] },
    { path: "register", component: RegisterComponent },
    { path: "login", component: LoginComponent },
    { path: "books", component: BooksComponent, canActivate: [authGuard] },
    { path: "authors", component: AuthorsComponent, canActivate: [authGuard] },
    { path: "add-author", component: AuhtorFormComponent, canActivate: [authGuard] },
    { path: "add-book", component: BookFormComponent, canActivate: [authGuard] },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
