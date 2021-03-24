export class SearchPlaysOptions {
  public skip;
  public take;
  public gamesIds: string[];
  public playersIds: string[];
  public dateFrom: Date | string;
  public dateTo: Date | string;
  public searchText: string;

  public constructor() {
    this.take = 10;
    this.skip = 0;
    this.gamesIds = [];
    this.playersIds = [];
    this.dateFrom = '';
    this.dateTo = '';
    this.searchText = '';
  }
}
