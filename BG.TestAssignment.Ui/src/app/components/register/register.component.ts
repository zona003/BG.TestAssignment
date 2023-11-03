import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/models/user';
import { UserRegister } from 'src/app/models/userRegister';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registrationForm: FormGroup;
  userRegister: UserRegister | undefined;

  constructor(
    private router : Router,
    private fb: FormBuilder,
    private authService: AuthService) {
    this.registrationForm = this.fb.group({
      username: ['', Validators.required],
      password:['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      birthdate: ['', Validators.required],
      address: ['', Validators.required],
    },
    );
  }

  onSubmit() {
    const userData = this.registrationForm.value;
    this.userRegister = new UserRegister(userData.usename, userData.password, userData.firstname, userData.lastname, userData.password, userData.address);

    if (this.registrationForm.valid) {
      if(this.authService.registration(this.userRegister)){
        this.router.navigateByUrl('/');
      }
    }
  }

  register(){
    const userData = this.registrationForm.value;
    var utcDate = (userData.birthdate as Date).toISOString();
    this.userRegister = new UserRegister(userData.username, userData.password, userData.firstname, userData.lastname, utcDate, userData.address);
    console.log(this.userRegister);
    
    if (this.registrationForm.valid) {
      if(this.authService.registration(this.userRegister)){
        this.router.navigateByUrl('/');
      }
    }
  }

  cancell(){
    this.router.navigateByUrl('login');
  }
}
