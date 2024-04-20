export class SearchPlayerStatsOptions {
  public take?: number;
  public skip?: number;
  public gameId?: string | null;
  public playerId?: string | null;
  public sortBy?: PlayerStatsSortBy;
}

export enum PlayerStatsSortBy {
  TotalPlayed,
  TotalWins,
  AverageScore
}
