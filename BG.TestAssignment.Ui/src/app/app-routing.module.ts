import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './components/books/books.component';
import { AuthorsComponent } from './components/authors/authors.component';
import { UserComponent } from './components/user/user.component';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: "", component: UserComponent },
  { path: "books", component: BooksComponent, canActivate: [authGuard] },
  { path: "authors", component: AuthorsComponent, canActivate: [authGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
