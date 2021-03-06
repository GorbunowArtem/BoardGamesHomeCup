import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { GamesConstraints } from '../common/models/constraints';
import { PagedSearchResponse } from '../common/paged-search-response';
import { Game } from './models/game';
import { GameDetails } from './models/game-details';
import { SearchGamesOptions } from './models/search-games-options';

const gamesUrl = 'https://localhost:5004/games';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  public stateChangedSubject: Subject<any> = new Subject();

  private _defaultOptions = new SearchGamesOptions();
  private _options!: SearchGamesOptions;
  private _constraints!: GamesConstraints;

  public constructor(private _httpModule: HttpClient) {}

  public init() {
    if (!this._constraints) {
      this._httpModule
        .get<GamesConstraints>(`${gamesUrl}/constraints`)
        .subscribe((c) => (this._constraints = c));
    }
  }

  public isTitleUnique(title: string, id: string | undefined): Observable<any> {
    return this._httpModule.head(`${gamesUrl}?title=${title}&id=${id}`, { observe: 'response' });
  }

  public search(options: SearchGamesOptions): Observable<PagedSearchResponse<Game>> {
    this._options = options;

    return this._httpModule.get<PagedSearchResponse<Game>>(`${gamesUrl}`, {
      params: options as any
    });
  }

  public add(game: Game) {
    return this._httpModule
      .post<Game>(`${gamesUrl}`, game)
      .pipe(map(() => this.stateChangedSubject.next(this._defaultOptions)));
  }

  public edit(model: Game): Observable<void> {
    return this._httpModule
      .patch<Game>(`${gamesUrl}/${model.id}`, model)
      .pipe(map(() => this.stateChangedSubject.next(this._defaultOptions)));
  }

  public get(id: string | null | undefined): Observable<GameDetails> {
    return this._httpModule.get<any>(`${gamesUrl}/${id}`);
  }

  public delete(id: string | undefined): Observable<any> {
    return this._httpModule
      .delete(`${gamesUrl}/${id}`)
      .pipe(map(() => this.stateChangedSubject.next(this._defaultOptions)));
  }

  public stateChanged(): Observable<any> {
    return this.stateChangedSubject.asObservable();
  }

  public get currentOptions() {
    return this._options;
  }

  public get constraints(): GamesConstraints {
    return this._constraints;
  }
}
