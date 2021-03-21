import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { RegisterDialogComponent } from './register/register-dialog.component';
import { MatInputModule } from '@angular/material/input';
import { UniqueLoginValidatorDirective } from './register/unique-login-validator.directive';
import { UniqueEmailValidatorDirective } from './register/unique-email-validator.directive';
import { HcCommonModule } from '../common/hc-common.module';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';

@NgModule({
  declarations: [
    AuthCallbackComponent,
    RegisterDialogComponent,
    UniqueLoginValidatorDirective,
    UniqueEmailValidatorDirective
  ],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatDialogModule,
    HcCommonModule
  ],
  exports: [RegisterDialogComponent]
})
export class AccountModule {}
