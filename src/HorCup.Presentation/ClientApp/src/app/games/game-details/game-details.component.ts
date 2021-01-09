import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GamesService } from '../games.service';
import { GameDetails } from '../models/game-details';

@Component({
  selector: 'hc-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
  public gameDetails!: GameDetails;

  public constructor(
    private _gamesService: GamesService,
    private _activatedRoute: ActivatedRoute
  ) {}

  public ngOnInit() {
    this._activatedRoute.paramMap.subscribe((params) => {
      const gameId = params.get('id');

      this._gamesService.getDetails(gameId).subscribe((game) => (this.gameDetails = game));
    });
  }
}
