import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';

import { HcAvatarComponent } from './hc-avatar.component';

describe('HcAvatarComponent', () => {
  let component: HcAvatarComponent;
  let fixture: ComponentFixture<HcAvatarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HcAvatarComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HcAvatarComponent);
    component = fixture.componentInstance;
  });

  it('should initials to equal T N', () => {
    component.name = 'Test Name';
    fixture.detectChanges();

    const initials = fixture.debugElement.query(By.css('.initials'));

    expect(initials.nativeElement.textContent).toBe(' TN ');
  });

  it('should initials to be "PL"', () => {
    component.name = 'Player';
    fixture.detectChanges();

    const initals = fixture.debugElement.query(By.css('.initials'));

    expect(initals.nativeElement.textContent).toBe('PL');
  });

  it('should initials to be empty string', () => {
    const initals = fixture.debugElement.query(By.css('.initials'));

    expect(initals.nativeElement.textContent).toBe('');
  });
});
