export class SearchPlayerStatsOptions {
  public constructor(
    public take?: number,
    public skip?: number,
    public gameId?: string | null,
    public sortBy?: PlayerStatsSortBy
  ) {}
}

export enum PlayerStatsSortBy {
  TotalPlayed,
  TotalWins,
  AverageScore
}
