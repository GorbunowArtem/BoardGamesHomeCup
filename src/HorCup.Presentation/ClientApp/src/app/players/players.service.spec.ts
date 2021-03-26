import { of } from 'rxjs';
import { SearchPlayersOptions } from './models/search-players-options';
import { PlayersService } from './players.service';
import { testPlayer1 } from './test-data/test-player';

describe('Service: Players', () => {
  let playersService: PlayersService;
  let httpClientMock: any;

  beforeEach(() => {
    httpClientMock = jasmine.createSpyObj('HttpClient', {
      head: of(),
      get: of(),
      post: of(),
      patch: of()
    });
    playersService = new PlayersService(httpClientMock);
  });

  it('should verify is nickname unique', () => {
    playersService.isNicknameUnique('nick', 'id').subscribe();

    expect(httpClientMock.head).toHaveBeenCalledWith('/players?nickname=nick&id=id', {
      observe: 'response'
    });
  });

  it('should add player', () => {
    playersService.add(testPlayer1).subscribe();

    expect(httpClientMock.post).toHaveBeenCalledWith('/players', testPlayer1);
  });

  it('should search players', () => {
    const searchOptions = new SearchPlayersOptions(3, 5, 'text');

    playersService.search(searchOptions);

    expect(httpClientMock.get).toHaveBeenCalledWith('/players', { params: searchOptions });
  });

  it('should edit player', () => {
    playersService.edit(testPlayer1).subscribe();

    expect(httpClientMock.patch).toHaveBeenCalledWith(`/players/${testPlayer1.id}`, testPlayer1);
  });
});
