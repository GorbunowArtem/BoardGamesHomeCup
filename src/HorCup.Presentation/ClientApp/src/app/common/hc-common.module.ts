import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';

import { HcFieldValidationErrorsComponent } from './field-validation-errors/field-validation-errors.component';
import { HcAvatarComponent } from './hc-avatar/hc-avatar.component';

@NgModule({
  imports: [CommonModule, MatFormFieldModule],
  declarations: [HcFieldValidationErrorsComponent, HcAvatarComponent],
  exports: [HcFieldValidationErrorsComponent, HcAvatarComponent]
})
export class HcCommonModule {}
