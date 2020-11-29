import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/internal/Subject';
import { map } from 'rxjs/operators';
import { PagedSearchResponse } from '../common/paged-search-response';
import { Player } from './models/player';
import { PlayerConstraints } from './models/player-constraints';
import { SearchPlayersOptions } from './models/search-players-options';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  public _playerAddedSubject: Subject<any> = new Subject();

  constructor(private _http: HttpClient) {}

  public getConstraints(): Observable<PlayerConstraints> {
    return this._http.get<PlayerConstraints>('/players/constraints');
  }

  public isNicknameUnique(nickname: string): Observable<any> {
    return this._http.head(`/players?nickname=${nickname}`, { observe: 'response' });
  }

  public addPlayer(player: Player) {
    return this._http
      .post<Player>('players', player)
      .pipe(map(() => this._playerAddedSubject.next({ added: player.firstName })));
  }

  public search(options: SearchPlayersOptions) {
    return this._http.get<PagedSearchResponse<Player>>(
      `/players?take=${options.take}&skip=${options.skip}&searchText=${options.searchText}`
    );
  }

  public playerAdded(): Observable<any> {
    return this._playerAddedSubject.asObservable();
  }
}
