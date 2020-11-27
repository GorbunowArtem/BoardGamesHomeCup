import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { PagedSearchResponse } from '../common/paged-search-response';
import { Player } from './models/player';
import { PlayerConstraints } from './models/player-constraints';
import { SearchPlayersOptions } from './models/search-players-options';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  constructor(private _http: HttpClient) {}

  public getConstraints(): Observable<PlayerConstraints> {
    return this._http.get<PlayerConstraints>('/players/constraints');
  }

  public isNicknameUnique(nickname: string): Observable<any> {
    return this._http.head(`/players?nickname=${nickname}`, { observe: 'response' });
  }

  public addPlayer(player: Player) {
    return this._http.post<Player>('players', player);
  }

  public search(options: SearchPlayersOptions) {
    return this._http.get<PagedSearchResponse<Player>>(
      `/players?take=${options.take}&skip=${options.skip}&searchText=${options.searchText}`
    );
  }
}
