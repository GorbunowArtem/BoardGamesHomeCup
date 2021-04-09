import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Subscription } from 'rxjs';
import { PlayerSource } from '../players/players-source';
import { AddEditGameDialogComponent } from './add-edit-game-dialog/add-edit-game-dialog.component';
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

  public gamesSource: PlayerSource;

  public total: number;

  private _searchParamsChangedSubscription!: Subscription;

  private _searchOptions: SearchGamesOptions;

  public constructor(private _gamesService: GamesService, private _addEditGameDialog: MatDialog) {
    this.games = [];
    this.total = 0;
    this._searchOptions = new SearchGamesOptions(6);
    this.gamesSource = new PlayerSource(_gamesService);
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

  public openAddDialog() {
    this._addEditGameDialog.open(AddEditGameDialogComponent, {
      disableClose: true
    });
  }
}
