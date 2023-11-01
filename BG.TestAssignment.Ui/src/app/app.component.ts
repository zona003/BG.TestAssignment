import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

constructor(private as:AuthService) {
}

  public get isLoggedIn(): boolean{
    return this.as.isAutenticated();
  }

  login(userName: string, password: string){
    this.as.login(userName, password)
    .subscribe({ error: (e) => alert('Wrong login or password.')
    });
  }

  logout(){
    this.as.logout();
  }
  
}
