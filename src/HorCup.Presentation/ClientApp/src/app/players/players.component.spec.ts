import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HarnessLoader } from '@angular/cdk/testing';
import { TestbedHarnessEnvironment } from '@angular/cdk/testing/testbed';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonHarness } from '@angular/material/button/testing';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PlayersComponent } from './players.component';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';
import { PlayersService } from './players.service';
import { of, Subject } from 'rxjs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatPaginatorHarness } from '@angular/material/paginator/testing';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SearchPlayersOptions } from './models/search-players-options';
import { HeaderCardMockComponent } from '../common/test-data/header-card-mock';
import { MatIconModule } from '@angular/material/icon';
import { Component, Input } from '@angular/core';
import { Player } from './models/player';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'hc-player-card',
  template: `<div>Player</div>`
})
export class PlayerCardMockComponent {
  @Input()
  public player!: Player;
}

describe('PlayersComponent', () => {
  let fixture: ComponentFixture<PlayersComponent>;
  let matDialogMock: any;
  let loader: HarnessLoader;
  let playersServiceMock: PlayersService;

  beforeEach(async () => {
    playersServiceMock = jasmine.createSpyObj(PlayersService, {
      playerAdded: new Subject().asObservable(),
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
      countChanged: of()
    });
    matDialogMock = jasmine.createSpyObj('MatDialog', ['open']);

    await TestBed.configureTestingModule({
      declarations: [PlayersComponent, HeaderCardMockComponent, PlayerCardMockComponent],
      providers: [
        { provide: PlayersService, useValue: playersServiceMock },
        { provide: MatDialog, useValue: matDialogMock }
      ],
      imports: [
        MatDialogModule,
        MatButtonModule,
        MatPaginatorModule,
        MatFormFieldModule,
        MatIconModule,
        MatToolbarModule
      ]
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

  it('should navigate to next page and load next set of players', async () => {
    const paginator = await loader.getHarness(MatPaginatorHarness);

    (playersServiceMock.search as any).calls.reset();

    await paginator.goToNextPage();

    expect(playersServiceMock.search).toHaveBeenCalledWith(new SearchPlayersOptions(6, 6));
  });
});
