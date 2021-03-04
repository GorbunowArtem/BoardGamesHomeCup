import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { RegisterDialogComponent } from './register/register-dialog.component';
import { LoginComponent } from './login/login.component';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [RegisterDialogComponent, LoginComponent],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatDialogModule
  ],
  exports: [RegisterDialogComponent]
})
export class AccountModule {}
