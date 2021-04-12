import { IPaginatedSearchOptions } from 'src/app/common/models/paginated-search-options';

export class SearchPlaysOptions implements IPaginatedSearchOptions {
  public skip;
  public take;
  public gamesIds: string[];
  public playersIds: string[];
  public dateFrom: Date | string;
  public dateTo: Date | string;
  public searchText: string;

  public constructor() {
    this.take = 15;
    this.skip = 0;
    this.gamesIds = [];
    this.playersIds = [];
    this.dateFrom = '';
    this.dateTo = '';
    this.searchText = '';
  }
}
