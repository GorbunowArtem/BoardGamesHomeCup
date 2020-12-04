import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatBottomSheet, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { PageEvent } from '@angular/material/paginator';
import { Subscription } from 'rxjs';
import { GamesFilterComponent } from './games-filter/games-filter.component';
import { GamesService } from './games.service';
import { Game } from './models/game';
import { SearchGamesOptions } from './models/search-games-options';

@Component({
  selector: 'hc-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent implements OnInit, OnDestroy {
  games!: Game[];

  total!: number;

  private _searchParamsChangedSubscription!: Subscription;

  private _searchOptions = new SearchGamesOptions(6);

  constructor(private _gamesService: GamesService, private _bottomSheet: MatBottomSheet) {}

  ngOnInit() {
    this.search();
    this._searchParamsChangedSubscription = this._gamesService.searchParamsChangedSubject.subscribe(
      (options) => {
        this._searchOptions = options;
        this.search();
      }
    );
  }
  ngOnDestroy() {
    this._searchParamsChangedSubscription.unsubscribe();
  }

  pageChangedEvent(event: PageEvent) {
    window.scrollTo(0, 0);
    this._searchOptions.take = event.pageSize;
    this._searchOptions.skip = event.pageSize * event.pageIndex;

    this.search();
  }

  public search() {
    this._gamesService.search(this._searchOptions).subscribe((result) => {
      this.games = result.items;
      this.total = result.total;
    });
  }

  public openFilter() {
    this._bottomSheet.open(GamesFilterComponent, {
      data: this._searchOptions
    });
  }
}
