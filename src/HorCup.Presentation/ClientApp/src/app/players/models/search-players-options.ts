export class SearchPlayersOptions {
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
