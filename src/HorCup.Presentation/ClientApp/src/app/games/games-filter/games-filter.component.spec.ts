/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { GamesFilterComponent } from './games-filter.component';

xdescribe('GamesFilterComponent', () => {
  let component: GamesFilterComponent;
  let fixture: ComponentFixture<GamesFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GamesFilterComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GamesFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
