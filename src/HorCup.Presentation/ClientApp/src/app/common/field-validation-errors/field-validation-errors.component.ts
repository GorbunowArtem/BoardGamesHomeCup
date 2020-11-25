import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HcValidationMessage } from '../validation-messages/validation-messages';

@Component({
  selector: 'hc-field-validation-errors',
  templateUrl: './field-validation-errors.component.html'
})
export class HcFieldValidationErrorsComponent {
  @Input() messages!: HcValidationMessage[];

  @Input() fieldName!: string;

  @Input() form!: FormGroup;
}
