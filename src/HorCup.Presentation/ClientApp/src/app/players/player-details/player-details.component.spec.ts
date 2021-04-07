import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { PlayersService } from '../players.service';
import { testPlayer1 } from '../test-data/test-player';
import { PlayerDetailsComponent } from './player-details.component';
import { RouterTestingModule } from '@angular/router/testing';
import { MockComponent } from 'ng-mocks';
import { HeaderCardComponent } from 'src/app/common/header-card/header-card.component';
import { MatCardModule } from '@angular/material/card';
import { TopGamesStatsComponent } from './top-games-stats/top-games-stats.component';

describe('PlayerDetailsComponent', () => {
  let component: PlayerDetailsComponent;
  let fixture: ComponentFixture<PlayerDetailsComponent>;
  let playersServiceMock: PlayersService;
  let activatedRouteMock: ActivatedRoute;

  beforeEach(async () => {
    playersServiceMock = jasmine.createSpyObj(PlayersService, {
      get: of(testPlayer1),
      getStats: of({
        items: {
          $values: []
        }
      })
    });
    activatedRouteMock = jasmine.createSpyObj(ActivatedRoute, {
      paramMap: of({ id: 'playerId' })
    });

    await TestBed.configureTestingModule({
      declarations: [
        PlayerDetailsComponent,
        MockComponent(HeaderCardComponent),
        MockComponent(TopGamesStatsComponent)
      ],
      providers: [
        { provide: PlayersService, useValue: playersServiceMock },
        { provide: activatedRouteMock, useValue: ActivatedRoute }
      ],
      imports: [RouterTestingModule, MatCardModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerDetailsComponent);
    component = fixture.componentInstance;
    //  component.player = testPlayer1;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
