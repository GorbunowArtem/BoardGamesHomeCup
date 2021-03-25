import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { PagedSearchResponse } from '../common/paged-search-response';
import { Game } from './models/game';
import { GameDetails } from './models/game-details';
import { SearchGamesOptions } from './models/search-games-options';

const gamesUrl = '/games';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  public searchParamsChangedSubject = new Subject<SearchGamesOptions>();

  public constructor(private _httpModule: HttpClient) {}

  public isTitleUnique(title: string, id: string | undefined): Observable<any> {
    return this._httpModule.head(`${gamesUrl}?title=${title}&id=${id}`, { observe: 'response' });
  }

  public search(options: SearchGamesOptions): Observable<PagedSearchResponse<Game>> {
    return this._httpModule.get<PagedSearchResponse<Game>>(`${gamesUrl}`, {
      params: options as any
    });
  }

  public add(game: Game) {
    return this._httpModule
      .post<Game>(`${gamesUrl}`, game)
      .pipe(map(() => this.searchParamsChangedSubject.next(new SearchGamesOptions())));
  }

  public edit(model: Game): Observable<void> {
    return this._httpModule
      .patch<Game>(`${gamesUrl}/${model.id}`, model)
      .pipe(map(() => this.searchParamsChangedSubject.next(new SearchGamesOptions())));
  }

  public get(id: string | null | undefined): Observable<GameDetails> {
    return this._httpModule.get<any>(`${gamesUrl}/${id}`);
  }

  public delete(id: string | undefined): Observable<any> {
    return this._httpModule
      .delete(`${gamesUrl}/${id}`)
      .pipe(map(() => this.searchParamsChangedSubject.next(new SearchGamesOptions())));
  }
}
