/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PlaysNavBarComponent } from './plays-nav-bar.component';

describe('PlaysNavBarComponent', () => {
  let component: PlaysNavBarComponent;
  let fixture: ComponentFixture<PlaysNavBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PlaysNavBarComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlaysNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
