import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { finalize } from 'rxjs/operators';
import { AuthService } from 'src/app/common/auth.service';
import {
  min,
  NotUnique,
  RequiredField
} from 'src/app/common/validation-messages/validation-messages';

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
  public errorMessages: any;

  public constructor(
    private _authService: AuthService,
    private _fb: FormBuilder,
    private _matDialogRef: MatDialogRef<RegisterDialogComponent>
  ) {
    this.registerForm = _fb.group({
      name: ['', { updateOn: 'blur' }, [Validators.required]],
      email: ['', { updateOn: 'blur' }, [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });

    this.errorMessages = {
      name: [RequiredField, NotUnique],
      email: [RequiredField, NotUnique],
      password: [RequiredField, min(6)]
    };
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
