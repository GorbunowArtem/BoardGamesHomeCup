import { Component, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { GamesFilterComponent } from '../games-filter/games-filter.component';
import { SearchGamesOptions } from '../models/search-games-options';

@Component({
  selector: 'hc-games-nav-bar',
  templateUrl: './games-nav-bar.component.html',
  styleUrls: ['./games-nav-bar.component.scss']
})
export class GamesNavBarComponent {
  public constructor(private _gamesFilter: MatBottomSheet) {}

  public openFilter() {
    this._gamesFilter.open(GamesFilterComponent, {
      data: new SearchGamesOptions()
    });
  }
}
