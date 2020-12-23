export class SearchPlayersOptions {
  public constructor(
    public take: number = 10,
    public skip: number = 0,
    public searchText: string = '',
    public exceptIds: string[] = []
  ) {}
}
