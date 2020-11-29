export class SearchPlayersOptions {
  constructor(public take: number = 10, public skip: number = 0, public searchText: string = '') {}
}
