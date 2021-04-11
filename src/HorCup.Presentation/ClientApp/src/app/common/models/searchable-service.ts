import { Observable } from 'rxjs';
import { PagedSearchResponse } from '../paged-search-response';
import { IPaginatedSearchOptions } from './paginated-search-options';

export interface ISearchableService {
  search(searchOptions: IPaginatedSearchOptions): Observable<PagedSearchResponse<any>>;
  stateChanged(): Observable<any>;
}
