import { OverlayContainer } from '@angular/cdk/overlay';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialogHarness } from '@angular/material/dialog/testing';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserTestingModule } from '@angular/platform-browser/testing';
import { Subject } from 'rxjs';
import { HeaderCardMockComponent } from 'src/app/common/test-data/header-card-mock';
import { NavBarMockComponent } from 'src/app/nav-bar/test-data/nav-bar-header-mock';
import { GamesComponent } from '../games.component';
import { GamesService } from '../games.service';
import { GameCardMockComponent } from '../test-data/game-card-mock';
import { GamesFilterComponent } from './games-filter.component';

describe('GamesFilterComponent', () => {
  let component: GamesComponent;
  let fixture: ComponentFixture<GamesComponent>;
  let rootLoader: HarnessLoader;
  let overlayContainer: OverlayContainer;
  let gamesServiceMock;

  beforeEach(async () => {
    gamesServiceMock = {
      searchParamsChangedSubject: new Subject<any>()
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
      providers: [{ provide: GamesService, useValue: gamesServiceMock }]
    })
      .overrideModule(BrowserTestingModule, {
        set: {
          entryComponents: [GamesFilterComponent]
        }
      })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GamesComponent);
    component = fixture.componentInstance;
    rootLoader = TestbedHarnessEnvironment.documentRootLoader(fixture);

    overlayContainer = TestBed.inject(OverlayContainer);
    component.openFilter();

    fixture.detectChanges();
  });

  afterEach(async () => {
    const dialogs = await rootLoader.getAllHarnesses(MatDialogHarness);
    await Promise.all(dialogs.map(async (d) => await d.close()));

    // Angular won't call this for us so we need to do it ourselves to avoid leaks.
    overlayContainer.ngOnDestroy();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
