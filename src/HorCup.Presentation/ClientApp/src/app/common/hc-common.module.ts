import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';

import { HcFieldValidationErrorsComponent } from './field-validation-errors/field-validation-errors.component';

@NgModule({
  imports: [CommonModule, MatFormFieldModule],
  declarations: [HcFieldValidationErrorsComponent],
  exports: [HcFieldValidationErrorsComponent]
})
export class HcCommonModule {}
