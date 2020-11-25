/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FieldValidationErrorsComponent } from './field-validation-errors.component';

describe('FieldValidationErrorsComponent', () => {
  let component: FieldValidationErrorsComponent;
  let fixture: ComponentFixture<FieldValidationErrorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [FieldValidationErrorsComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FieldValidationErrorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
