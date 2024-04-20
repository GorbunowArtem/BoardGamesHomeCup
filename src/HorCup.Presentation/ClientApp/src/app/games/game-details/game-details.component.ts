import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlayerStatistic } from 'src/app/players/models/player-statistic';
import {
  PlayerStatsSortBy,
  SearchPlayerStatsOptions
} from 'src/app/players/models/search-player-stats-options';
import { PlayersService } from 'src/app/players/players.service';
import { GamesService } from '../games.service';
import { GameDetails } from '../models/game-details';

@Component({
  selector: 'hc-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
  public gameDetails!: GameDetails;
  public topPlayedPlayers: PlayerStatistic[] = [];
  public topWinnersPlayers: PlayerStatistic[] = [];
  public topAverageScore: PlayerStatistic[] = [];

  public constructor(
    private _gamesService: GamesService,
    private _activatedRoute: ActivatedRoute,
    private _playersService: PlayersService
  ) {}

  public ngOnInit() {
    this._activatedRoute.paramMap.subscribe((params) => {
      const gameId = params.get('id');

      this._gamesService.get(gameId).subscribe((game) => (this.gameDetails = game));

      this._playersService
        .getStats({
          sortBy: PlayerStatsSortBy.TotalPlayed,
          take: 3,
          gameId: gameId
        })
        .subscribe((stats) => (this.topPlayedPlayers = stats.items.$values));

      this._playersService
        .getStats({
          sortBy: PlayerStatsSortBy.TotalWins,
          take: 3,
          gameId: gameId
        })
        .subscribe((stats) => (this.topWinnersPlayers = stats.items.$values));

      this._playersService
        .getStats({
          sortBy: PlayerStatsSortBy.AverageScore,
          take: 3,
          gameId: gameId
        })
        .subscribe((stats) => (this.topAverageScore = stats.items.$values));
    });
  }
}
