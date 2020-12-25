import { PlayersService } from '../../players.service';
import { testPlayer1 } from '../../test-data/test-player';
import { EditPlayerStrategy } from './edit-player-strategy';

describe('EditPlayerStrategy', () => {
  let playersServiceMock: PlayersService;
  let sut: EditPlayerStrategy;

  beforeEach(() => {
    playersServiceMock = jasmine.createSpyObj(PlayersService, ['edit']);
    sut = new EditPlayerStrategy(testPlayer1, playersServiceMock);
  });

  it('should call edit method of playerService on save call', () => {
    sut.save(testPlayer1);

    expect(playersServiceMock.edit).toHaveBeenCalledWith(testPlayer1);
  });

  it('should return "Изменения сохранены" on successMessage', () => {
    expect(sut.successMessage).toBe('Изменения сохранены');
  });

  it('should return player', () => {
    expect(sut.model).toEqual(testPlayer1);
  });
});
