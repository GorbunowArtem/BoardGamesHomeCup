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

  @HostListener('window:scroll', ['$event'])
  public onScroll() {
    if (window.innerHeight + window.scrollY === document.body.scrollHeight && !this.loading) {
      this.loadMore();
    }
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
  private _length = 100000;
  private _pageSize = 100;
  private _fetchedPages = new Set<number>();
  private _dataStream = new BehaviorSubject<Player[]>([]);
  private _subscription = new Subscription();

  public connect(collectionViewer: CollectionViewer): Observable<Player[]> {
    this._subscription.add(
      collectionViewer.viewChange.subscribe((range) => {
        const startPage = this._getPageForIndex(range.start);
        const endPage = this._getPageForIndex(range.end - 1);
        for (let i = startPage; i <= endPage; i++) {
          this._fetchPage(i);
        }
      })
    );
    return this._dataStream;
  }

  public constructor(private _playerService: PlayersService) {
    super();
  }
  public disconnect(): void {
    this._subscription.unsubscribe();
  }

  private _getPageForIndex(index: number): number {
    return Math.floor(index / this._pageSize);
  }

  private _fetchPage(page: number) {
    if (this._fetchedPages.has(page)) {
      return;
    }
    this._fetchedPages.add(page);

    this._playerService.search(new SearchPlayersOptions()).subscribe((players) => {
      this._dataStream.next(players.items.$values);
    });
  }
}
