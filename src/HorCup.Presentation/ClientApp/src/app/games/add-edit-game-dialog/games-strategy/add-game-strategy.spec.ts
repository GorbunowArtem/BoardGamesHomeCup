import { GamesService } from '../../games.service';
import { testGame1 } from '../../test-data/test-game';
import { AddGameStrategy } from './add-game-strategy';

describe('AddGameStrategy', () => {
  let sut: AddGameStrategy;
  let gamesServiceMock: any;

  beforeEach(() => {
    gamesServiceMock = jasmine.createSpyObj(GamesService, ['add']);
    sut = new AddGameStrategy(gamesServiceMock);
  });

  it('should call gamesService.add on save', () => {
    sut.save(testGame1);

    expect(gamesServiceMock.add).toHaveBeenCalledWith(testGame1);
  });

  it('should return "Игра добавлена" as success message', () => {
    expect(sut.successMessage).toBe('Игра добавлена');
  });

  it('should return empty form model as model', () => {
    expect(sut.model).toEqual({
      id: undefined,
      maxPlayers: '',
      minPlayers: '',
      title: ''
    });
  });
});
