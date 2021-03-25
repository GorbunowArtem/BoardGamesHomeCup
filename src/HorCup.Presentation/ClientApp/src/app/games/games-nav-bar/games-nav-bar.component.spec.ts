import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatBottomSheet, MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatToolbarHarness } from '@angular/material/toolbar/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GamesFilterComponent } from '../games-filter/games-filter.component';
import { SearchGamesOptions } from '../models/search-games-options';
import { GamesNavBarComponent } from './games-nav-bar.component';

describe('GamesNavBarComponent', () => {
  let fixture: ComponentFixture<GamesNavBarComponent>;
  let loader: HarnessLoader;
  let bottomSheetMock: any;
  let toolbar: MatToolbarHarness;

  beforeEach(async () => {
    bottomSheetMock = jasmine.createSpyObj(['open']);

    TestBed.configureTestingModule({
      imports: [
        MatToolbarModule,
        MatButtonModule,
        MatIconModule,
        BrowserAnimationsModule,
        MatBottomSheetModule
      ],
      declarations: [GamesNavBarComponent],
      providers: [{ provide: MatBottomSheet, useValue: bottomSheetMock }]
    }).compileComponents();

    fixture = TestBed.createComponent(GamesNavBarComponent);
    fixture.detectChanges();
    loader = TestbedHarnessEnvironment.loader(fixture);
    toolbar = await loader.getHarness(MatToolbarHarness);
  });

  it('should have menu button', async () => {
    const menuButtons = await toolbar.getAllHarnesses(MatButtonHarness.with({ text: 'menu' }));

    expect(menuButtons.length).toBe(1);
  });

  it('should open filter', async () => {
    const filterBtn = await toolbar.getHarness(MatButtonHarness.with({ text: 'filter_list' }));

    await filterBtn.click();

    expect(bottomSheetMock.open).toHaveBeenCalledWith(GamesFilterComponent, {
      data: new SearchGamesOptions()
    });
  });
});
