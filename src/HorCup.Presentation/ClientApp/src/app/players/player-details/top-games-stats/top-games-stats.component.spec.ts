/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TopGamesStatsComponent } from './top-games-stats.component';

describe('TopGamesStatsComponent', () => {
  let component: TopGamesStatsComponent;
  let fixture: ComponentFixture<TopGamesStatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [TopGamesStatsComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopGamesStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
