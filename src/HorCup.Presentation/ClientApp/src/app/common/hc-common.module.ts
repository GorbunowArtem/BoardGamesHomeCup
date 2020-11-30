import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';

import { HcFieldValidationErrorsComponent } from './field-validation-errors/field-validation-errors.component';
import { HcAvatarComponent } from './hc-avatar/hc-avatar.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  imports: [CommonModule, MatFormFieldModule, MatDialogModule, MatButtonModule, MatIconModule],
  declarations: [HcFieldValidationErrorsComponent, HcAvatarComponent, ConfirmationDialogComponent],
  exports: [HcFieldValidationErrorsComponent, HcAvatarComponent, ConfirmationDialogComponent]
})
export class HcCommonModule {}
