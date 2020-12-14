import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { PagedSearchResponse } from '../common/paged-search-response';
import { toHttpParams } from '../services-utils';
import { Game } from './models/game';
import { GamesConstraints } from './models/games-constraints';
import { SearchGamesOptions } from './models/search-games-options';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  public searchParamsChangedSubject = new Subject<SearchGamesOptions>();

  constructor(private _httpModule: HttpClient) {}

  public search(options: SearchGamesOptions): Observable<PagedSearchResponse<Game>> {
    return this._httpModule.get<PagedSearchResponse<Game>>('/games', {
      params: toHttpParams(options)
    });
  }
  public getConstraints() {
    return this._httpModule.get<GamesConstraints>('/games/constraints');
  }

  public add(game: Game) {
    return this._httpModule
      .post<Game>('/games', game)
      .pipe(map(() => this.searchParamsChangedSubject.next(new SearchGamesOptions())));
  }

  public get(id: string | null): Observable<any> {
    return this._httpModule.get<any>(`/games/${id}`);
  }
}
