import { of } from 'rxjs';
import { Player } from './models/player';
import { SearchPlayersOptions } from './models/search-players-options';
import { PlayersService } from './players.service';

describe('Service: Players', () => {
  let playersService: PlayersService;
  let httpClientMock: any;

  const player: Player = {
    firstName: 'first',
    lastName: 'last',
    nickname: 'nick',
    birthDate: 'Thu Dec 24 2020 20:32:07'
  };

  beforeEach(() => {
    httpClientMock = jasmine.createSpyObj('HttpClient', ['head', 'get', 'post', 'put']);

    playersService = new PlayersService(httpClientMock);
  });

  it('should verify is nickname unique', () => {
    playersService.isNicknameUnique('nick').subscribe();

    expect(httpClientMock.head).toHaveBeenCalledWith('/players?nickname=nick', {
      observe: 'response'
    });
  });

  it('should add player', () => {
    playersService.add(player).subscribe();

    expect(httpClientMock.post).toHaveBeenCalledWith('players', player);
  });

  it('should search players', () => {
    const searchOptions = new SearchPlayersOptions(3, 5, 'text');

    playersService.search(searchOptions);

    expect(httpClientMock.get).toHaveBeenCalledWith('/players', { params: searchOptions });
  });

  it('should edit player', () => {
    playersService.edit(player).subscribe();

    expect(httpClientMock.put).toHaveBeenCalledWith('/player', player);
  });
});
