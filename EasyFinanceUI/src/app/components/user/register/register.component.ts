import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user/user.service';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { first } from 'rxjs/operators';
import { MustMatch } from 'src/app/helpers/must-match';
import { MatSnackBar } from '@angular/material';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  hide: boolean = true;
  registerForm: FormGroup;

  constructor(private userSvc: UserService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]]
    },
      { validator: MustMatch('password', 'confirmPassword') })
  }

  // convenience getter for easy access to form fields
  get controls() { return this.registerForm.controls; }

  onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    const user: User = {
      id: null,
      firstName: this.controls.firstName.value,
      lastName: this.controls.lastName.value,
      email: this.controls.email.value,
      password: this.controls.password.value,
      roleName: null
    }

    this.userSvc.register(user)
      .pipe(first())
      .subscribe(() => {
        this.showNotification('Ваш аккаунт успішно створено.', 'Закрити');
        this.router.navigate(['/login'])
      },
      error => {
        this.showNotification('Помилка! Не вдалося створити новий аккаунт.', 'Закрити')
      });
  }

  showNotification(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
