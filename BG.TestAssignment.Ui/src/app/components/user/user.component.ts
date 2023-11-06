import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {
  myuser: User = new User(0, '', '', '', new Date(), '');

  constructor(
    private users: UserService,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getCuttentUser();
  }

  getCuttentUser() {
    this.users.getCurrentUser().subscribe((us) => {
      this.myuser = us;
    });
  }

  public get isLoggedIn(): boolean {
    return this.auth.isAutenticated();
  }

  logout() {
    this.auth.logout();
    this.router.navigateByUrl('/login');
  }
}
