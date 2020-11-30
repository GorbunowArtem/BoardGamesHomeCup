import { of } from 'rxjs';
import { Player } from './models/player';
import { SearchPlayersOptions } from './models/search-players-options';
import { PlayersService } from './players.service';

describe('Service: Players', () => {
  let playersService: PlayersService;
  let httpClientMock: any;

  beforeEach(() => {
    httpClientMock = httpClientMock = jasmine.createSpyObj('HttpClient', {
      head: of(),
      get: of(),
      post: of()
    });
    playersService = new PlayersService(httpClientMock);
  });

  it('should get players constraints', () => {
    playersService.getConstraints().subscribe();

    expect(httpClientMock.get).toHaveBeenCalledWith('/players/constraints');
  });

  it('should verify is nickname unique', () => {
    playersService.isNicknameUnique('nick').subscribe();

    expect(httpClientMock.head).toHaveBeenCalledWith('/players?nickname=nick', {
      observe: 'response'
    });
  });

  it('should add player', () => {
    const player: Player = {
      firstName: 'first',
      lastName: 'last',
      nickname: 'nick',
      birthDate: new Date('12.12.1989')
    };

    playersService.add(player).subscribe();

    expect(httpClientMock.post).toHaveBeenCalledWith('players', player);
  });

  it('should search players', () => {
    const searchOptions = new SearchPlayersOptions(3, 5, 'text');

    playersService.search(searchOptions);

    expect(httpClientMock.get).toHaveBeenCalledWith(
      `/players?take=${searchOptions.take}&skip=${searchOptions.skip}&searchText=${searchOptions.searchText}`
    );
  });
});
