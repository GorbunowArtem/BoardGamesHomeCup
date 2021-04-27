import { OverlayContainer } from '@angular/cdk/overlay';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialogHarness } from '@angular/material/dialog/testing';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSlideToggleHarness } from '@angular/material/slide-toggle/testing';
import { BrowserDynamicTestingModule } from '@angular/platform-browser-dynamic/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ThemeService } from '../../theme.service';
import { UserSettingsComponent } from '../user-settings.component';
import { UserSettingsDialogComponent } from './user-settings-dialog.component';

describe('UserSettingsDialogComponent', () => {
  let component: UserSettingsComponent;
  let fixture: ComponentFixture<UserSettingsComponent>;
  let rootLoader: HarnessLoader;
  let overlayContainer: OverlayContainer;
  let themeServiceMock: ThemeService;

  beforeEach(async () => {
    themeServiceMock = jasmine.createSpyObj('ThemesService', ['setDarkTheme']);

    await TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        MatButtonModule,
        MatIconModule,
        MatSlideToggleModule,
        MatDialogModule
      ],
      declarations: [UserSettingsDialogComponent],
      providers: [{ provide: ThemeService, useValue: themeServiceMock }]
    })
      .overrideModule(BrowserDynamicTestingModule, {
        set: {
          entryComponents: [UserSettingsDialogComponent]
        }
      })
      .compileComponents();

    fixture = TestBed.createComponent(UserSettingsComponent);
    component = fixture.componentInstance;

    rootLoader = TestbedHarnessEnvironment.documentRootLoader(fixture);
    overlayContainer = TestBed.inject(OverlayContainer);

    component.openDialog();
    fixture.detectChanges();
  });

  afterEach(async () => {
    const dialogs = await rootLoader.getAllHarnesses(MatDialogHarness);

    await Promise.all(dialogs.map(async (d: MatDialogHarness) => await d.close()));
    overlayContainer.ngOnDestroy();
  });

  it('should toggle dark theme', async () => {
    const themesSwitcher = await rootLoader.getHarness(MatSlideToggleHarness);

    await themesSwitcher.toggle();

    expect(themeServiceMock.setDarkTheme).toHaveBeenCalledWith(true);

    await themesSwitcher.toggle();

    expect(themeServiceMock.setDarkTheme).toHaveBeenCalledWith(false);
  });
});
