import { PlayersService } from '../../players.service';
import { testPlayer1 } from '../../test-data/test-player';
import { AddPlayerStrategy } from './add-player-strategy';

describe('AddPlayerStrategy', () => {
  let playersServiceMock: PlayersService;
  let sut: AddPlayerStrategy;

  beforeEach(() => {
    playersServiceMock = jasmine.createSpyObj(PlayersService, ['add']);
    sut = new AddPlayerStrategy(playersServiceMock);
  });

  it('should call add method of playerService on save call', () => {
    sut.save(testPlayer1);

    expect(playersServiceMock.add).toHaveBeenCalledWith(testPlayer1);
  });

  it('should return "Игрок добавлен" on successMessage', () => {
    expect(sut.successMessage).toBe('Игрок добавлен');
  });

  it('should return player with all empty values', () => {
    const emptyPlayer = {
      birthDate: '',
      firstName: '',
      lastName: '',
      nickname: '',
      id: undefined
    };

    expect(sut.player).toEqual(emptyPlayer);
  });
});
