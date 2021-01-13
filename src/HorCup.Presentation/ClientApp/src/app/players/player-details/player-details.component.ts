import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PlayerDetails } from '../models/player-details';
import { PlayerStatistic } from '../models/player-statistic';
import { PlayerStatsSortBy } from '../models/search-player-stats-options';
import { PlayersService } from '../players.service';

@Component({
  selector: 'hc-player-details',
  templateUrl: './player-details.component.html',
  styleUrls: ['./player-details.component.scss']
})
export class PlayerDetailsComponent implements OnInit {
  public player!: PlayerDetails;

  public topPlayedGames: PlayerStatistic[];
  public topWinningGames: PlayerStatistic[];

  public constructor(
    private _playersService: PlayersService,
    private _activatedRoute: ActivatedRoute
  ) {
    this.topPlayedGames = [];
    this.topWinningGames = [];
  }

  public ngOnInit() {
    this._activatedRoute.paramMap.subscribe((params) => {
      const playerId = params.get('id');

      this._playersService.get(playerId).subscribe((player) => (this.player = player));

      this._playersService
        .getStats({
          playerId: playerId,
          take: 5,
          sortBy: PlayerStatsSortBy.TotalPlayed
        })
        .subscribe((stats) => (this.topPlayedGames = stats.items.$values));

      this._playersService
        .getStats({
          playerId: playerId,
          take: 5,
          sortBy: PlayerStatsSortBy.TotalWins
        })
        .subscribe((stats) => (this.topWinningGames = stats.items.$values));
    });
  }
}
