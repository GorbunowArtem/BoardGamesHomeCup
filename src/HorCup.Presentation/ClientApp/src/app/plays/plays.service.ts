import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ISearchableService } from '../common/models/searchable-service';
import { PagedSearchResponse } from '../common/paged-search-response';
import { AddPlay } from './models/add-play';
import { Play } from './models/play';
import { SearchPlaysOptions } from './models/search-plays-options';

const PlaysUrl = 'https://localhost:5005/plays';
@Injectable({
  providedIn: 'root'
})
export class PlaysService implements ISearchableService {
  private _stateChangedSubject = new Subject<SearchPlaysOptions>();

  public constructor(private _http: HttpClient) {}

  public get searchParamsChangedSubject() {
    return this._stateChangedSubject;
  }

  public search(options: SearchPlaysOptions): Observable<PagedSearchResponse<Play>> {
    return this._http.get<PagedSearchResponse<Play>>(PlaysUrl, {
      params: options as any
    });
  }

  public add(play: AddPlay): Observable<AddPlay> {
    return this._http.post<AddPlay>(PlaysUrl, play);
  }

  public stateChanged(): Observable<any> {
    return this._stateChangedSubject.asObservable();
  }
}
