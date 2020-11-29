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
    component.name = 'Test Name';
    fixture.detectChanges();
  });

  it('should initials to equal T N', () => {
    const initials = fixture.debugElement.query(By.css('.initials'));

    expect(initials.nativeElement.textContent).toBe(' TN ');
  });
});
