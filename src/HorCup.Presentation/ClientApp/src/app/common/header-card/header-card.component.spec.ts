import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { HarnessLoader } from '@angular/cdk/testing';
import { MatCardModule } from '@angular/material/card';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { MatIconModule } from '@angular/material/icon';
import { Location } from '@angular/common';
import { HeaderCardComponent } from './header-card.component';
import { By } from '@angular/platform-browser';

describe('HeaderCardComponent', () => {
  let component: HeaderCardComponent;
  let fixture: ComponentFixture<HeaderCardComponent>;
  let loader: HarnessLoader;
  let locationStub: any;

  beforeEach(async () => {
    locationStub = {
      back: jasmine.createSpy('back')
    };

    await TestBed.configureTestingModule({
      imports: [MatCardModule, MatButtonModule, MatIconModule],
      declarations: [HeaderCardComponent],
      providers: [{ provide: Location, useValue: locationStub }]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderCardComponent);
    loader = TestbedHarnessEnvironment.loader(fixture);
    component = fixture.componentInstance;
  });

  it('should show header text', () => {
    const headerText = 'Header text';

    component.text = headerText;
    fixture.detectChanges();

    const expected = fixture.debugElement.query(By.css('.header-text'));

    expect(expected.nativeElement.textContent).toBe(headerText);
  });

  it('should navigate to previous page when user clicks to back button', async () => {
    const backButton = await loader.getHarness(MatButtonHarness);

    await backButton.click();

    const location = fixture.debugElement.injector.get(Location);

    expect(location.back).toHaveBeenCalled();
  });

  it('should hide back button if "hideNav" property set to false', async () => {
    let backButton = await loader.getAllHarnesses(MatButtonHarness);

    expect(backButton.length).toBe(1);

    component.hideNav = true;

    fixture.detectChanges();

    backButton = await loader.getAllHarnesses(MatButtonHarness);

    expect(backButton.length).toBe(0);
  });
});
