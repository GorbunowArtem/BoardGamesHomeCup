import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'hc-field-validation-errors',
  templateUrl: './field-validation-errors.component.html'
})
export class HcFieldValidationErrorsComponent {
  @Input() messages!: any;

  @Input() fieldName!: any;

  @Input() form!: FormGroup;
}
