/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PlayPanelComponent } from './play-panel.component';

xdescribe('PlayPanelComponent', () => {
  let component: PlayPanelComponent;
  let fixture: ComponentFixture<PlayPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PlayPanelComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
