import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
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

  total!: number;

  constructor(private _gamesService: GamesService) {}

  ngOnInit() {
    this.search();
  }

  pageChangedEvent(event: PageEvent) {
    this.search(event.pageSize, event.pageSize * event.pageIndex);
  }

  public search(take: number = 5, skip: number = 0) {
    this._gamesService.search(new SearchGamesOptions(take, skip)).subscribe((result) => {
      this.games = result.items;
      this.total = result.total;
    });
  }
}
