import { Component, OnInit } from '@angular/core';
import { GamesService } from './games.service';
import { Game } from './models/game';
import { SearchGamesOptions } from './models/search-games-options';

@Component({
  selector: 'hc-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent implements OnInit {
  games!: Game[];

  constructor(private _gamesService: GamesService) {}

  ngOnInit() {
    this._gamesService.search(new SearchGamesOptions()).subscribe((games) => {
      this.games = games.items;
    });
  }

  applyFilter(event: Event) {}
}
