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
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { PlayersService } from '../players.service';
import { MatButtonHarness } from '@angular/material/button/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { of, Subject } from 'rxjs';
import { PlayerConstraints } from '../models/player-constraints';
import { MatIconModule } from '@angular/material/icon';
import { Component, Input } from '@angular/core';
import { MatNativeDateModule, MatRippleModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { HcValidationMessage } from 'src/app/common/validation-messages/validation-messages';
import { MatInputModule } from '@angular/material/input';
import { MatInputHarness } from '@angular/material/input/testing';
import { PagedSearchResponse } from 'src/app/common/paged-search-response';
import { Player } from '../models/player';

@Component({ selector: 'hc-field-validation-errors', template: '' })
class ValidationErrorsStubComponent {
  @Input() public messages!: HcValidationMessage[];

  @Input() public fieldName!: string;

  @Input() public form!: FormGroup;
}
@Component({ selector: 'hc-nav-bar', template: '' })
class NavBarStubComponent {}

const validName = 'Name';
const notValidFirstName = 'NotValid';

const validLastName = 'Last';
const notValidLastName = 'Notvalidlast';

const validNickname = 'Nick';
const notValidNickname = 'Nickkkk';

const validDate = '1997-12-17T03:24:00';
const notValidDate = '1995-12-16T03:24:00';

const playerConstraints: PlayerConstraints = {
  maxNameLength: 5,
  minBirthDate: '1995-12-17T03:24:00'
};

const searchPlayersResponse: PagedSearchResponse<Player> = {
  items: [
    {
      birthDate: new Date('1995-12-17T03:24:00'),
      firstName: 'Test',
      lastName: 'Player',
      nickname: 'Test P'
    }
  ],
  total: 1
};
describe('AddEditPlayerDialogComponent', () => {
  let component: PlayersComponent;
  let fixture: ComponentFixture<PlayersComponent>;
  let rootLoader: HarnessLoader;
  let overlayContainer: OverlayContainer;
  let playersServiceMock: PlayersService;
  let formBuilder: FormBuilder;

  beforeEach(async () => {
    formBuilder = new FormBuilder();

    playersServiceMock = jasmine.createSpyObj(PlayersService, {
      getConstraints: of(playerConstraints),
      add: of(),
      search: of(searchPlayersResponse),
      playerAdded: new Subject().asObservable()
    });
    await TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        MatDialogModule,
        MatButtonModule,
        MatDatepickerModule,
        MatFormFieldModule,
        MatIconModule,
        MatRippleModule,
        MatNativeDateModule,
        ReactiveFormsModule,
        FormsModule,
        MatFormFieldModule,
        MatInputModule
      ],
      declarations: [
        PlayersComponent,
        AddEditPlayerDialogComponent,
        ValidationErrorsStubComponent,
        NavBarStubComponent
      ],
      providers: [
        { provide: PlayersService, useValue: playersServiceMock },
        { provide: FormBuilder, useValue: formBuilder },
        { provide: MAT_DATE_LOCALE, useValue: 'ru-RU' }
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

    component.showDialog();

    fixture.detectChanges();
  });

  afterEach(async () => {
    const dialogs = await rootLoader.getAllHarnesses(MatDialogHarness);

    await Promise.all(dialogs.map(async (d: MatDialogHarness) => await d.close()));
    overlayContainer.ngOnDestroy();
  });

  describe('Form validation', () => {
    it('should "Добавить" button be disabled if form is invalid', async () => {
      const firstNameField = await getFirstNameField();

      firstNameField.setValue(notValidFirstName);

      const saveBtn = await getSaveButton();

      expect(saveBtn.isDisabled()).toBeTruthy();
    });

    it('should first name field to be required', async () => {
      const firstNameField = await getFirstNameField();

      const isRequired = await firstNameField.isRequired();

      expect(isRequired).toEqual(true);
    });

    it('should last name field to be required', async () => {
      const lastNameField = await getLastNameField();

      const isRequired = await lastNameField.isRequired();

      expect(isRequired).toEqual(true);
    });

    it('should nickname field to be required', async () => {
      const nicknameField = await getNicknameField();

      const isRequired = await nicknameField.isRequired();

      expect(isRequired).toEqual(true);
    });

    it('should birth date field to be required', async () => {
      const birthDateField = await getBirthDateField();

      const isRequired = await birthDateField.isRequired();

      expect(isRequired).toEqual(true);
    });
  });

  it('should add player if all inputs are valid', async () => {
    const firstNameField = await getFirstNameField();

    await firstNameField.setValue(validName);

    const lastNameField = await getLastNameField();

    await lastNameField.setValue(validLastName);

    const nickNameField = await getNicknameField();

    await nickNameField.setValue(validNickname);

    const birthDateField = await getBirthDateField();

    await birthDateField.setValue(validDate);

    fixture.detectChanges();

    const saveBtn = await getSaveButton();

    await saveBtn.click();

    fixture.detectChanges();

    expect(playersServiceMock.add).toHaveBeenCalledWith({
      firstName: validName,
      lastName: validLastName,
      nickname: validNickname,
      birthDate: new Date(validDate)
    });
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

  async function getSaveButton() {
    return await rootLoader.getHarness(
      MatButtonHarness.with({
        text: 'Добавить'
      })
    );
  }

  async function getBirthDateField() {
    return await rootLoader.getHarness(
      MatInputHarness.with({
        placeholder: 'Дата рождения'
      })
    );
  }

  async function getNicknameField() {
    return await rootLoader.getHarness(MatInputHarness.with({ placeholder: 'Ник' }));
  }

  async function getLastNameField() {
    return await rootLoader.getHarness(
      MatInputHarness.with({
        placeholder: 'Фамилия'
      })
    );
  }

  async function getFirstNameField() {
    return await rootLoader.getHarness(
      MatInputHarness.with({
        placeholder: 'Имя'
      })
    );
  }
});
