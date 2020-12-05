export class SearchGamesOptions {
  public constructor(
    public take: number = 5,
    public skip: number = 0,
    public searchText: string = '',
    public minPlayers?: number,
    public maxPlayers?: number
  ) {}
}
