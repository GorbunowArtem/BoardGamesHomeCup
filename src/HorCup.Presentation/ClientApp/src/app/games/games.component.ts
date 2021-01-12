import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Subscription } from 'rxjs';
import { AddEditGameDialogComponent } from './add-edit-game-dialog/add-edit-game-dialog.component';
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
  public games: Game[];

  public total: number;

  private _searchParamsChangedSubscription!: Subscription;

  private _searchOptions: SearchGamesOptions;

  public constructor(
    private _gamesService: GamesService,
    private _gamesFilter: MatBottomSheet,
    private _addEditGameDialog: MatDialog
  ) {
    this.games = [];
    this.total = 0;
    this._searchOptions = new SearchGamesOptions(6);
  }

  public ngOnInit() {
    this.search();
    this._searchParamsChangedSubscription = this._gamesService.searchParamsChangedSubject.subscribe(
      (options) => {
        this._searchOptions = options;
        this.search();
      }
    );
  }
  public ngOnDestroy() {
    this._searchParamsChangedSubscription.unsubscribe();
  }

  public pageChangedEvent(event: PageEvent) {
    this._searchOptions.take = event.pageSize;
    this._searchOptions.skip = event.pageSize * event.pageIndex;

    this.search();
  }

  public search() {
    this._gamesService.search(this._searchOptions).subscribe((result) => {
      this.games = result.items.$values;
      this.total = result.total;
      window.scrollTo(0, 0);
    });
  }

  public openFilter() {
    this._gamesFilter.open(GamesFilterComponent, {
      data: this._searchOptions
    });
  }

  public openAddDialog() {
    this._addEditGameDialog.open(AddEditGameDialogComponent, {
      disableClose: true
    });
  }
}
