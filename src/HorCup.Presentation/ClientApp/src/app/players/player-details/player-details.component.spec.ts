import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { PlayersService } from '../players.service';
import { testPlayer1 } from '../test-data/test-player';
import { PlayerDetailsComponent } from './player-details.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('PlayerDetailsComponent', () => {
  let component: PlayerDetailsComponent;
  let fixture: ComponentFixture<PlayerDetailsComponent>;
  let playersServiceMock: PlayersService;
  let activatedRouteMock: ActivatedRoute;

  beforeEach(async () => {
    playersServiceMock = jasmine.createSpyObj(PlayersService, {
      get: of(testPlayer1)
    });
    activatedRouteMock = jasmine.createSpyObj(ActivatedRoute, {
      paramMap: of({ id: 'playerId' })
    });

    await TestBed.configureTestingModule({
      declarations: [PlayerDetailsComponent],
      providers: [
        { provide: PlayersService, useValue: playersServiceMock },
        { provide: activatedRouteMock, useValue: ActivatedRoute }
      ],
      imports: [RouterTestingModule]
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
