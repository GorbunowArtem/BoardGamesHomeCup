/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PlaysFilterComponent } from './plays-filter.component';

xdescribe('PlaysFilterComponent', () => {
  let component: PlaysFilterComponent;
  let fixture: ComponentFixture<PlaysFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PlaysFilterComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaysFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
