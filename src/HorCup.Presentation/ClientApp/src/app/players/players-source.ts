import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { Player } from './models/player';
import { SearchPlayersOptions } from './models/search-players-options';
import { PlayersService } from './players.service';

export class PlayerSource extends DataSource<Player> {
  private _loading = false;
  private _cachedPlayers = Array.from<Player>({ length: 0 });
  private _dataStream = new BehaviorSubject<Player[]>(this._cachedPlayers);
  private _subscription = new Subscription();
  private _searchOptions = new SearchPlayersOptions();
  private _pageSize = 15;
  private _lastPage = 0;
  private _total = 0;
  private _playersCountChangedSubscription!: Subscription;

  public constructor(private _playersService: PlayersService) {
    super();
    this.search();
    this._playersCountChangedSubscription = _playersService.stateChanged().subscribe(() => {
      this._cachedPlayers = [];
      this._searchOptions = new SearchPlayersOptions();
      this.search();
    });
  }

  public connect(collectionViewer: CollectionViewer): Observable<Player[]> {
    this._subscription.add(
      collectionViewer.viewChange.subscribe((range) => {
        const currentPage = this._getPageForIndex(range.end);

        if (currentPage > this._lastPage) {
          this._lastPage = currentPage;
          this._searchOptions.skip = this._pageSize * this._lastPage;
          this.search();
        }
      })
    );
    return this._dataStream;
  }
  public disconnect(): void {
    this._subscription.unsubscribe();
    this._playersCountChangedSubscription.unsubscribe();
  }

  private _getPageForIndex(index: number): number {
    return Math.floor(index / this._pageSize);
  }

  private search() {
    this._loading = true;
    this._playersService.search(this._searchOptions).subscribe((players) => {
      this._total = players.total;
      this._cachedPlayers = this._cachedPlayers.concat(players.items.$values);
      this._dataStream.next(this._cachedPlayers);
      this._loading = false;
    });
  }

  public get total() {
    return this._total;
  }

  public get loading() {
    return this._loading;
  }
}
