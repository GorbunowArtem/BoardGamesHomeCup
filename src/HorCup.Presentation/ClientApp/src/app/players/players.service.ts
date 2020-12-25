import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/internal/Subject';
import { map } from 'rxjs/operators';
import { PagedSearchResponse } from '../common/paged-search-response';
import { Player } from './models/player';
import { PlayerDetails } from './models/player-details';
import { SearchPlayersOptions } from './models/search-players-options';

const PlayersUrl = '/players';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  private _countChangedSubject: Subject<any> = new Subject();

  public constructor(private _http: HttpClient) {}

  public isNicknameUnique(nickname: string, id: string | undefined): Observable<any> {
    return this._http.head(`${PlayersUrl}?nickname=${nickname}&id=${id}`, { observe: 'response' });
  }

  public add(player: Player): Observable<void> {
    return this._http
      .post<Player>(`${PlayersUrl}`, player)
      .pipe(map(() => this._countChangedSubject.next({ added: player.id })));
  }

  public edit(player: Player): Observable<void> {
    return this._http
      .patch<Player>(`${PlayersUrl}/${player.id}`, player)
      .pipe(map(() => this._countChangedSubject.next({ edited: player.id })));
  }

  public search(options: SearchPlayersOptions) {
    return this._http.get<PagedSearchResponse<Player>>(`${PlayersUrl}`, {
      params: options as any
    });
  }

  public delete(id: string | undefined): Observable<any> {
    return this._http
      .delete(`${PlayersUrl}/${id}`)
      .pipe(map(() => this._countChangedSubject.next({ removed: id })));
  }

  public countChanged(): Observable<any> {
    return this._countChangedSubject.asObservable();
  }

  public get(id: string | null): Observable<PlayerDetails> {
    return this._http.get<PlayerDetails>(`${PlayersUrl}/${id}`);
  }
}
