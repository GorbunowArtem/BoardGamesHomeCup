import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PagedSearchResponse } from '../common/paged-search-response';
import { AddPlay } from './models/add-play';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';

@Injectable({
  providedIn: 'root'
})
export class PlaysService {
  public constructor(private _http: HttpClient) {}

  public search(options: SearchPlaysOptions): Observable<PagedSearchResponse<Play>> {
    return this._http.get<PagedSearchResponse<Play>>('/plays', {
      params: options as any
    });
  }

  public add(play: AddPlay): Observable<AddPlay> {
    return this._http.post<AddPlay>('/plays', play);
  }
}
