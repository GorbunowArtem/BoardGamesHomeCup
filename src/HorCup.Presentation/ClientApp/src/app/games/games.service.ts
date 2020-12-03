import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedSearchResponse } from '../common/paged-search-response';
import { toHttpParams } from '../services-utils';
import { Game } from './models/game';
import { SearchGamesOptions } from './models/search-games-options';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  constructor(private _httpModule: HttpClient) {}

  public search(options: SearchGamesOptions): Observable<any> {
    return this._httpModule.get<PagedSearchResponse<Game>>('/games', {
      params: toHttpParams(options)
    });
  }
}
