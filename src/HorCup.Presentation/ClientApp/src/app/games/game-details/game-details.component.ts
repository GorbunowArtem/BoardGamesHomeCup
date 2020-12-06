import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GamesService } from '../games.service';

@Component({
  selector: 'hc-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
  public game: any;

  constructor(private _gamesService: GamesService, private _activatedRoute: ActivatedRoute) {}

  ngOnInit() {
    this._activatedRoute.paramMap.subscribe((params) => {
      const gameId = params.get('id');

      this._gamesService.get(gameId).subscribe((game) => (this.game = game));
    });
  }
}
