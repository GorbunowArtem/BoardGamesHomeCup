import { OverlayContainer } from '@angular/cdk/overlay';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserDynamicTestingModule } from '@angular/platform-browser-dynamic/testing';
import { PlayersComponent } from '../players.component';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog.component';
import { MatDialogHarness } from '@angular/material/dialog/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { PlayersService } from '../players.service';
import { MatButtonHarness } from '@angular/material/button/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { of, Subject } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { Component, Input } from '@angular/core';
import { MatRippleModule } from '@angular/material/core';
import { HcValidationMessage } from 'src/app/common/validation-messages/validation-messages';
import { MatInputModule } from '@angular/material/input';
import { MatInputHarness } from '@angular/material/input/testing';
import { PagedSearchResponse } from 'src/app/common/paged-search-response';
import { Player } from '../models/player';
import { NavBarMockComponent } from 'src/app/nav-bar/test-data/nav-bar-header-mock';
import { HeaderCardMockComponent } from 'src/app/common/test-data/header-card-mock';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonService } from 'src/app/common/common.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { PlayersPersistenceFactory } from './player-strategy/players-persistence-factory';
import { testPlayer1 } from '../test-data/test-player';

@Component({
  selector: 'hc-player-card',
  template: `<div>Player</div>`
})
export class PlayerCardComponent {
  @Input()
  public player!: Player;
}
@Component({ selector: 'hc-field-validation-errors', template: '' })
class ValidationErrorsStubComponent {
  @Input() public messages!: HcValidationMessage[];

  @Input() public fieldName!: string;

  @Input() public form!: FormGroup;
}
const validNickname = 'Nick';

const searchPlayersResponse: PagedSearchResponse<Player> = {
  items: {
    $values: [
      {
        nickname: 'Test P'
      }
    ]
  },
  total: 1
};
describe('AddEditPlayerDialogComponent', () => {
  let component: PlayersComponent;
  let fixture: ComponentFixture<PlayersComponent>;
  let rootLoader: HarnessLoader;
  let overlayContainer: OverlayContainer;
  let playersServiceMock: PlayersService;
  let formBuilder: FormBuilder;
  let commonServiceMock: any;
  let persistenceFactoryMock: PlayersPersistenceFactory;
  let playersStrategyMock: any;

  beforeEach(async () => {
    formBuilder = new FormBuilder();

    playersServiceMock = jasmine.createSpyObj(PlayersService, {
      add: of(),
      stateChanged: of(),
      search: of(searchPlayersResponse),
      playerAdded: new Subject().asObservable()
    });

    commonServiceMock = {
      constraints: {
        playerConstraints: {
          maxNameLength: 11,
          minBirthDate: new Date()
        }
      }
    };

    playersStrategyMock = {
      save: new Subject().asObservable(),
      model: testPlayer1,
      successMessage: 'success'
    };

    persistenceFactoryMock = jasmine.createSpyObj(PlayersPersistenceFactory, {
      getStrategy: playersStrategyMock
    });

    await TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        MatDialogModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatRippleModule,
        ReactiveFormsModule,
        FormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatToolbarModule,
        MatSnackBarModule
      ],
      declarations: [
        PlayersComponent,
        AddEditPlayerDialogComponent,
        ValidationErrorsStubComponent,
        NavBarMockComponent,
        HeaderCardMockComponent,
        PlayerCardComponent
      ],
      providers: [
        { provide: PlayersService, useValue: playersServiceMock },
        { provide: FormBuilder, useValue: formBuilder },
        { provide: CommonService, useValue: commonServiceMock },
        { provide: PlayersPersistenceFactory, useValue: persistenceFactoryMock }
      ]
    })
      .overrideModule(BrowserDynamicTestingModule, {
        set: {
          entryComponents: [AddEditPlayerDialogComponent]
        }
      })
      .compileComponents();

    fixture = TestBed.createComponent(PlayersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    rootLoader = TestbedHarnessEnvironment.documentRootLoader(fixture);
    overlayContainer = TestBed.inject(OverlayContainer);

    component.addPlayer();

    fixture.detectChanges();
  });

  afterEach(async () => {
    const dialogs = await rootLoader.getAllHarnesses(MatDialogHarness);

    await Promise.all(dialogs.map(async (d: MatDialogHarness) => await d.close()));
    overlayContainer.ngOnDestroy();
  });

  describe('Form validation', () => {
    it('should nickname field to be required', async () => {
      const nicknameField = await rootLoader.getHarness(
        MatInputHarness.with({ placeholder: 'Имя' })
      );

      const isRequired = await nicknameField.isRequired();

      expect(isRequired).toEqual(true);
    });
  });

  it('should add player if all inputs are valid', async () => {
    spyOn(playersStrategyMock, 'save');

    const nickNameField = await rootLoader.getHarness(MatInputHarness.with({ placeholder: 'Имя' }));

    await nickNameField.setValue(validNickname);

    const saveBtn = await rootLoader.getHarness(
      MatButtonHarness.with({
        text: 'Сохранить'
      })
    );

    await saveBtn.click();

    expect(playersStrategyMock.save).toHaveBeenCalledWith({ id: '1234', nickname: 'Nick' });
  });

  it('should close dialog when user clicks "cancel" ', async () => {
    fixture.detectChanges();
    const cancelBtn = await rootLoader.getHarness(
      MatButtonHarness.with({
        text: 'Отмена'
      })
    );

    await cancelBtn.click();

    const dialogs = await rootLoader.getAllHarnesses(MatDialogHarness);

    expect(dialogs.length).toBe(0);
  });
});
