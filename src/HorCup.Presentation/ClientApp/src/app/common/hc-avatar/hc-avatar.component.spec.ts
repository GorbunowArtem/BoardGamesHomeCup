/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HcAvatarComponent } from './hc-avatar.component';

describe('HcAvatarComponent', () => {
  let component: HcAvatarComponent;
  let fixture: ComponentFixture<HcAvatarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [HcAvatarComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HcAvatarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
