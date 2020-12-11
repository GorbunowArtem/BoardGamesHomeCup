import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedSearchResponse } from '../common/paged-search-response';
import { toHttpParams } from '../services-utils';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';

@Injectable({
  providedIn: 'root'
})
export class PlaysService {
  public constructor(private _http: HttpClient) {}

  public get(options: SearchPlaysOptions): Observable<PagedSearchResponse<Play>> {
    return this._http.get<PagedSearchResponse<Play>>('/plays', {
      params: toHttpParams(options)
    });
  }
}
