import { IPaginatedSearchOptions } from 'src/app/common/models/paginated-search-options';

export class SearchGamesOptions implements IPaginatedSearchOptions {
  public take: number;
  public skip: number;
  public searchText: string;
  public minPlayers: number;
  public maxPlayers: number;
  public exceptIds: string[];

  public constructor() {
    this.take = 15;
    this.skip = 0;
    this.searchText = '';
    this.minPlayers = 1;
    this.maxPlayers = 24;
    this.exceptIds = [];
  }
}
