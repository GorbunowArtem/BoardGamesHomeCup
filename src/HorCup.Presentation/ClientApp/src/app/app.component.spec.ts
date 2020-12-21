import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { CommonService } from './common/common.service';
import { NavBarMockComponent } from './nav-bar/test-data/nav-bar-header-mock';

describe('AppComponent', () => {
  let commonServiceMock: any;

  beforeEach(async () => {
    commonServiceMock = jasmine.createSpyObj('CommonService', ['init']);

    await TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [AppComponent, NavBarMockComponent],
      providers: [{ provide: CommonService, useValue: commonServiceMock }]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'hor-cup'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('hor-cup');
  });
});
