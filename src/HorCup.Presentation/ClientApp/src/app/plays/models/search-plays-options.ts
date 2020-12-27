export class SearchPlaysOptions {
  public constructor(
    public skip = 0,
    public take = 10,
    public gamesIds: string[] = [],
    public playersIds: string[] = [],
    public dateFrom: Date | string = '',
    public dateTo: Date | string = ''
  ) {}
}
