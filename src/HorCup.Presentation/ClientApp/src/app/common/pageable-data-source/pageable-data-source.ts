import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { ISearchableService } from '../models/searchable-service';
import { Player } from '../../players/models/player';
import { SearchPlayersOptions } from '../../players/models/search-players-options';
import { IPageableModel } from '../models/pageable-model';

export class PageableDataSource extends DataSource<IPageableModel> {
  private _loading = false;
  private _cachedItems = Array.from<IPageableModel>({ length: 0 });
  private _dataStream = new BehaviorSubject<IPageableModel[]>(this._cachedItems);
  private _subscription = new Subscription();
  private _searchOptions = new SearchPlayersOptions();
  private _pageSize = 15;
  private _lastPage = 0;
  private _total = 0;
  private _itemsCountChangedSubscription!: Subscription;

  public constructor(private _searchableService: ISearchableService) {
    super();
    this.search();
    this._itemsCountChangedSubscription = _searchableService.stateChanged().subscribe(() => {
      this._cachedItems = [];
      this._searchOptions = new SearchPlayersOptions();
      this.search();
    });
  }

  public connect(collectionViewer: CollectionViewer): Observable<IPageableModel[]> {
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
    this._itemsCountChangedSubscription.unsubscribe();
  }

  private _getPageForIndex(index: number): number {
    return Math.floor(index / this._pageSize);
  }

  private search() {
    this._loading = true;
    this._searchableService.search(this._searchOptions).subscribe((players) => {
      this._total = players.total;
      this._cachedItems = this._cachedItems.concat(players.items.$values);
      this._dataStream.next(this._cachedItems);
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
