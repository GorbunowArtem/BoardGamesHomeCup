import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'hc-field-validation-errors',
  templateUrl: './field-validation-errors.component.html'
})
export class HcFieldValidationErrorsComponent {
  @Input() public messages!: any;

  @Input() public fieldName!: any;

  @Input() public form!: FormGroup;
}
