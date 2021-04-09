import { IPaginatedSearchOptions } from 'src/app/common/models/i-paginated-search-options';

export class SearchPlayersOptions implements IPaginatedSearchOptions {
  public take: number;
  public skip: number;
  public searchText: string;
  public exceptIds: string[];

  public constructor() {
    this.take = 15;
    this.skip = 0;
    this.searchText = '';
    this.exceptIds = [];
  }
}
