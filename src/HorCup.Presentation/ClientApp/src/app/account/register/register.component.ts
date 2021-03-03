import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { UserRegistration } from 'src/app/common/models/user-registration';
import { AuthService } from 'src/app/core/authentication/auth.service';

@Component({
  selector: 'hc-app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  public registerForm!: FormGroup;
  public success!: boolean;
  public error!: string;
  public userRegistration: UserRegistration = { name: '', email: '', password: '' };
  public submitted: any = false;

  public constructor(private _authService: AuthService, private _fb: FormBuilder) {
    _fb.group({
      name: [''],
      email: [Validators.email, ''],
      password: ['']
    });
  }

  public cancel() {}

  public register() {
    // show preloader
    this._authService
      .register(this.userRegistration)
      .pipe(
        finalize(() => {
          // hide preloader
        })
      )
      .subscribe(
        (result) => {
          if (result) {
            this.success = true;
          }
        },
        (error) => {
          this.error = error;
        }
      );
  }
}
