import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PlayersComponent } from './players.component';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';
import { PlayersService } from './players.service';
import { of, Subject } from 'rxjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { By } from '@angular/platform-browser';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatListModule } from '@angular/material/list';
import { BottomNavComponent } from '../common/bottom-nav/bottom-nav';
import { MockComponent } from 'ng-mocks';
import { AddItemComponent } from '../common/add-item/add-item.component';
import { PlayersNavBarComponent } from './players-nav-bar/players-nav-bar.component';
import { HeaderCardComponent } from '../common/header-card/header-card.component';

describe('PlayersComponent', () => {
  let fixture: ComponentFixture<PlayersComponent>;
  let matDialogMock: any;
  let loader: HarnessLoader;
  let playersServiceMock: PlayersService;

  beforeEach(async () => {
    playersServiceMock = jasmine.createSpyObj(PlayersService, {
      playerAdded: new Subject().asObservable(),
      stateChanged: new Subject().asObservable(),
      search: of({
        total: 10,
        items: [
          {
            firstName: 'first',
            lastName: 'last',
            nickname: 'nick',
            birthDate: new Date('12.12.1989')
          }
        ]
      }),
      countChanged: of(),
      init: of()
    });
    matDialogMock = jasmine.createSpyObj('MatDialog', ['open']);

    await TestBed.configureTestingModule({
      declarations: [
        PlayersComponent,
        MockComponent(HeaderCardComponent),
        MockComponent(BottomNavComponent),
        MockComponent(AddItemComponent),
        MockComponent(PlayersNavBarComponent)
      ],
      providers: [
        { provide: PlayersService, useValue: playersServiceMock },
        { provide: MatDialog, useValue: matDialogMock }
      ],
      imports: [
        MatDialogModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatToolbarModule,
        MatProgressBarModule,
        ScrollingModule,
        MatListModule
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayersComponent);
    loader = TestbedHarnessEnvironment.loader(fixture);
  });

  it('should display dialog when user clicks on "plus" icon', async () => {
    const childEl: AddItemComponent = fixture.debugElement.query(By.directive(AddItemComponent))
      .componentInstance;

    childEl.showAddDialog.emit();

    expect(matDialogMock.open).toHaveBeenCalledWith(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  });
});
