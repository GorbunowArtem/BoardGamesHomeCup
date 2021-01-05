export class SearchGamesOptions {
  public constructor(
    public take: number = 5,
    public skip: number = 0,
    public searchText: string = '',
    public minPlayers: number = 0,
    public maxPlayers: number = 50,
    public exceptIds: string[] = []
  ) {}
}
