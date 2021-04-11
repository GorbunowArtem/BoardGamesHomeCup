import { of } from 'rxjs';
import { GamesService } from './games.service';
import { SearchGamesOptions } from './models/search-games-options';
import { testGame1 } from './test-data/test-game';

describe('Service: Games', () => {
  let gamesService: GamesService;
  let httpClientMock: any;

  beforeEach(() => {
    httpClientMock = jasmine.createSpyObj('HttpClient', {
      get: of(),
      post: of(),
      head: of()
    });

    gamesService = new GamesService(httpClientMock);
  });

  it('should search games', () => {
    const searchOptions = new SearchGamesOptions();
    searchOptions.searchText = 'sometext';

    gamesService.search(searchOptions);

    expect(httpClientMock.get).toHaveBeenCalledWith('/games', { params: searchOptions });
  });

  it('should save game', () => {
    gamesService.add(testGame1);

    expect(httpClientMock.post).toHaveBeenCalledWith('/games', testGame1);
  });

  it('should return game by id', () => {
    gamesService.get(testGame1.id);

    expect(httpClientMock.get).toHaveBeenCalledWith(`/games/${testGame1.id}`);
  });

  it('should check if title is unique', () => {
    gamesService.isTitleUnique(testGame1.title, testGame1.id);

    expect(httpClientMock.head).toHaveBeenCalledWith(
      `/games?title=${testGame1.title}&id=${testGame1.id}`,
      {
        observe: 'response'
      }
    );
  });
});
