import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PlayersComponent } from './players.component';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';

describe('PlayersComponent', () => {
  let fixture: ComponentFixture<PlayersComponent>;
  let matDialogMock: any;
  let loader: HarnessLoader;

  beforeEach(async () => {
    matDialogMock = jasmine.createSpyObj('MatDialog', ['open']);

    await TestBed.configureTestingModule({
      declarations: [PlayersComponent],
      providers: [{ provide: MatDialog, useValue: matDialogMock }],
      imports: [MatDialogModule, MatButtonModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayersComponent);
    loader = TestbedHarnessEnvironment.loader(fixture);
  });

  it('should display dialog when user clicks on "plus" icon', async () => {
    const plusButton = await loader.getHarness(MatButtonHarness);

    await plusButton.click();

    expect(matDialogMock.open).toHaveBeenCalledWith(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  });
});
