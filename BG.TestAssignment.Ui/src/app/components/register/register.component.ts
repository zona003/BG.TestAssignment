import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/models/user';
import { UserRegister } from 'src/app/models/userRegister';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registrationForm: FormGroup;
  userRegister: UserRegister | undefined;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService) {
    this.registrationForm = this.fb.group({
      username: ['', Validators.required],
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
      this.authService.registration(this.userRegister)
    }
  }

}
