import { OverlayContainer } from '@angular/cdk/overlay';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatBottomSheetModule, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialogHarness } from '@angular/material/dialog/testing';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatInputHarness } from '@angular/material/input/testing';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserTestingModule } from '@angular/platform-browser/testing';
import { Subject } from 'rxjs';
import { HeaderCardMockComponent } from 'src/app/common/test-data/header-card-mock';
import { NavBarMockComponent } from 'src/app/nav-bar/test-data/nav-bar-header-mock';
import { GamesNavBarComponent } from '../games-nav-bar/games-nav-bar.component';
import { GamesComponent } from '../games.component';
import { GamesService } from '../games.service';
import { SearchGamesOptions } from '../models/search-games-options';
import { GameCardMockComponent } from '../test-data/game-card-mock';
import { GamesFilterComponent } from './games-filter.component';

describe('GamesFilterComponent', () => {
  let component: GamesNavBarComponent;
  let fixture: ComponentFixture<GamesNavBarComponent>;
  let rootLoader: HarnessLoader;
  let overlayContainer: OverlayContainer;
  let gamesServiceMock: any;
  let bottomSheetData: SearchGamesOptions;

  beforeEach(async () => {
    gamesServiceMock = {
      searchParamsChangedSubject: new Subject(),
      search: jasmine.createSpy().and.returnValue({
        subscribe: () => {
          return {
            items: [],
            total: 1
          };
        }
      }),
      add: jasmine.createSpy(),
      get: jasmine.createSpy()
    };

    bottomSheetData = {
      maxPlayers: 2,
      minPlayers: 3,
      searchText: '',
      exceptIds: [],
      skip: 3,
      take: 4
    };

    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        MatBottomSheetModule,
        MatDialogModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatButtonModule,
        MatSelectModule,
        MatCardModule,
        MatIconModule,
        MatInputModule,
        MatPaginatorModule,
        MatToolbarModule
      ],
      declarations: [
        GamesComponent,
        GamesFilterComponent,
        HeaderCardMockComponent,
        NavBarMockComponent,
        GameCardMockComponent
      ],
      providers: [
        { provide: GamesService, useValue: gamesServiceMock },
        { provide: MAT_BOTTOM_SHEET_DATA, useValue: bottomSheetData }
      ]
    })
      .overrideModule(BrowserTestingModule, {
        set: {
          entryComponents: [GamesFilterComponent]
        }
      })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GamesNavBarComponent);
    component = fixture.componentInstance;
    rootLoader = TestbedHarnessEnvironment.documentRootLoader(fixture);

    overlayContainer = TestBed.inject(OverlayContainer);
    component.openFilter();

    fixture.detectChanges();
  });

  afterEach(async () => {
    const dialogs = await rootLoader.getAllHarnesses(MatDialogHarness);
    await Promise.all(dialogs.map(async (d) => await d.close()));

    overlayContainer.ngOnDestroy();
  });

  describe('on "Сбросить" click', () => {
    it('should reset search text', async () => {
      const titleInput = await rootLoader.getHarness(MatInputHarness);

      await titleInput.setValue('Searchtext');

      let titleText = await titleInput.getValue();

      expect(titleText).toEqual('Searchtext');

      const resetButton = await rootLoader.getHarness(
        MatButtonHarness.with({
          text: 'Сбросить'
        })
      );

      await resetButton.click();

      titleText = await titleInput.getValue();

      expect(titleText).toEqual('');
    });
  });
});
