import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { PagedSearchResponse } from '../common/paged-search-response';
import { Game } from './models/game';
import { SearchGamesOptions } from './models/search-games-options';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  public searchParamsChangedSubject = new Subject<SearchGamesOptions>();

  public constructor(private _httpModule: HttpClient) {}

  public search(options: SearchGamesOptions): Observable<PagedSearchResponse<Game>> {
    return this._httpModule.get<PagedSearchResponse<Game>>('/games', {
      params: options as any
    });
  }

  public add(game: Game) {
    return this._httpModule
      .post<Game>('/games', game)
      .pipe(map(() => this.searchParamsChangedSubject.next(new SearchGamesOptions())));
  }

  public get(id: string | null): Observable<any> {
    return this._httpModule.get<any>(`/games/${id}`);
  }

  public delete(id: string): Observable<any> {
    return this._httpModule
      .delete(`/games/${id}`)
      .pipe(map(() => this.searchParamsChangedSubject.next(new SearchGamesOptions())));
  }
}
