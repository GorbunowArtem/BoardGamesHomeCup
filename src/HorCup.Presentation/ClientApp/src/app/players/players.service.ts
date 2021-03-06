import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { PlayerConstraints } from '../common/models/constraints';
import { ISearchableService } from '../common/models/searchable-service';
import { PagedSearchResponse } from '../common/paged-search-response';
import { Player } from './models/player';
import { PlayerDetails } from './models/player-details';
import { PlayerStatistic } from './models/player-statistic';
import { SearchPlayerStatsOptions } from './models/search-player-stats-options';
import { SearchPlayersOptions } from './models/search-players-options';

const PlayersUrl = 'https://localhost:5003/players';
const PlayersStatsUrl = '/players-statistic';

@Injectable({
  providedIn: 'root'
})
export class PlayersService implements ISearchableService {
  private _stateChangedSubject: Subject<any> = new Subject();
  private _constraints!: PlayerConstraints;

  public constructor(private _http: HttpClient) {}

  public init() {
    if (!this._constraints) {
      this._http
        .get<PlayerConstraints>(`${PlayersUrl}/constraints`)
        .subscribe((c) => (this._constraints = c));
    }
  }

  public isNicknameUnique(nickname: string, id: string | undefined): Observable<any> {
    return this._http.head(`${PlayersUrl}?nickname=${nickname}&id=${id}`, { observe: 'response' });
  }

  public add(player: Player): Observable<void> {
    return this._http
      .post<Player>(`${PlayersUrl}`, player)
      .pipe(map(() => this._stateChangedSubject.next({ added: player })));
  }

  public edit(player: Player): Observable<void> {
    return this._http
      .patch<Player>(`${PlayersUrl}/${player.id}`, player)
      .pipe(map(() => this._stateChangedSubject.next({ edited: player })));
  }

  public search(options: SearchPlayersOptions) {
    return this._http.get<PagedSearchResponse<Player>>(`${PlayersUrl}`, {
      params: options as any
    });
  }

  public delete(id: string | undefined): Observable<any> {
    return this._http
      .delete(`${PlayersUrl}/${id}`)
      .pipe(map(() => this._stateChangedSubject.next({ removed: { id } })));
  }

  public stateChanged(): Observable<any> {
    return this._stateChangedSubject.asObservable();
  }

  public get(id: string | null): Observable<PlayerDetails> {
    return this._http.get<PlayerDetails>(`${PlayersUrl}/${id}`);
  }

  public getStats(
    options: SearchPlayerStatsOptions
  ): Observable<PagedSearchResponse<PlayerStatistic>> {
    return this._http.get<PagedSearchResponse<PlayerStatistic>>(`${PlayersStatsUrl}`, {
      params: options as any
    });
  }

  public get constraints(): PlayerConstraints {
    return this._constraints;
  }
}
