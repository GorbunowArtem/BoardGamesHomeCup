import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { ChangeDetectionStrategy, Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Observable, of, Subscription } from 'rxjs';
import { ConfirmationDialogComponent } from '../common/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogModel } from '../common/models/confirmation-dialog-model';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';
import { Player } from './models/player';
import { SearchPlayersOptions } from './models/search-players-options';
import { PlayersService } from './players.service';

@Component({
  selector: 'hc-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlayersComponent implements OnInit, OnDestroy {
  public loading: boolean;

  public players: Player[];

  public playersSource;

  public totalItems: number;

  public playersCountChangedSubscription!: Subscription;

  private readonly _playersPerPage: number;

  private _searchOptions: SearchPlayersOptions;

  public constructor(
    private _dialog: MatDialog,
    private _playersService: PlayersService,
    public dialog: MatDialog
  ) {
    this.players = [];
    this.totalItems = 0;
    this.loading = false;
    this._searchOptions = new SearchPlayersOptions();
    this._playersPerPage = 15;
    this.playersSource = new PlayerSource(_playersService);
  }

  public addPlayer() {
    this._dialog.open(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  }

  public ngOnInit() {
    this.search();
    this.playersCountChangedSubscription = this._playersService.stateChanged().subscribe(() => {
      this.players = [];
      this._searchOptions = new SearchPlayersOptions();
      this.search();
    });
  }

  public ngOnDestroy() {
    this.playersCountChangedSubscription.unsubscribe();
  }

  public search() {
    this.loading = true;
    this._playersService.search(this._searchOptions).subscribe((result) => {
      this.players.push(...result.items.$values);
      this.totalItems = result.total;
      this.loading = false;
    });
  }

  public edit(player: Player) {
    this.dialog.open(AddEditPlayerDialogComponent, {
      data: player,
      disableClose: true
    });
  }

  public delete(id: string | undefined) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      disableClose: true,
      data: new ConfirmationDialogModel()
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._playersService.delete(id).subscribe();
      }
    });
  }

  private loadMore() {
    if (this.totalItems > this.players.length) {
      this._searchOptions.take += this._searchOptions.take;
      this._searchOptions.skip = this._searchOptions.take - this._playersPerPage;

      this.search();
    }
  }
}

export class PlayerSource extends DataSource<Player> {
  private _cachedPlayers = Array.from<Player>({ length: 0 });
  private _dataStream = new BehaviorSubject<Player[]>(this._cachedPlayers);
  private _subscription = new Subscription();
  private _searchOptions = new SearchPlayersOptions();
  private _pageSize = 15;
  private _lastPage = 0;

  public connect(collectionViewer: CollectionViewer): Observable<Player[]> {
    this._subscription.add(
      collectionViewer.viewChange.subscribe((range) => {
        const currentPage = this._getPageForIndex(range.end);

        if (currentPage > this._lastPage) {
          this._lastPage = currentPage;
          this._searchOptions.take += this._pageSize;
          this._searchOptions.skip = this._pageSize * this._lastPage;
          this.searchPlayers();
        }
      })
    );
    return this._dataStream;
  }

  public constructor(private _playerService: PlayersService) {
    super();
    this.searchPlayers();
  }
  public disconnect(): void {
    this._subscription.unsubscribe();
  }

  private _getPageForIndex(index: number): number {
    return Math.floor(index / this._pageSize);
  }

  private searchPlayers() {
    this._playerService.search(this._searchOptions).subscribe((players) => {
      this._cachedPlayers = this._cachedPlayers.concat(players.items.$values);
      this._dataStream.next(this._cachedPlayers);
    });
  }
}
