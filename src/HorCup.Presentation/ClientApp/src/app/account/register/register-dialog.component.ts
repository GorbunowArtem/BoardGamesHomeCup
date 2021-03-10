import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { finalize } from 'rxjs/operators';
import { UserRegistration } from 'src/app/common/models/user-registration';
import { AuthService } from 'src/app/common/auth.service';

@Component({
  selector: 'hc-app-register',
  templateUrl: './register-dialog.component.html',
  styleUrls: ['./register-dialog.component.scss']
})
export class RegisterDialogComponent {
  public registerForm!: FormGroup;
  public success!: boolean;
  public error!: string;
  public submitted: any = false;

  public constructor(
    private _authService: AuthService,
    private _fb: FormBuilder,
    private _matDialogRef: MatDialogRef<RegisterDialogComponent>
  ) {
    this.registerForm = _fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  public cancel() {
    this._matDialogRef.close();
  }

  public register() {
    // show preloader

    this._authService
      .register(this.registerForm.value)
      .pipe(
        finalize(() => {
          // hide preloader
        })
      )
      .subscribe(
        (result) => {
          if (result) {
            this.success = true;
            this._matDialogRef.close();
          }
        },
        (error) => {
          this.error = error;
        }
      );
  }
}
