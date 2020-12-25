import { GamesService } from '../../games.service';
import { testGame1 } from '../../test-data/test-game';
import { EditGameStrategy } from './edit-game-strategy';

describe('EditGameStrategy', () => {
  let sut: EditGameStrategy;
  let gamesServiceMock: any;

  beforeEach(() => {
    gamesServiceMock = jasmine.createSpyObj(GamesService, ['edit']);
    sut = new EditGameStrategy(testGame1, gamesServiceMock);
  });

  it('should call gamesService.edit on save', () => {
    sut.save(testGame1);

    expect(gamesServiceMock.edit).toHaveBeenCalledWith(testGame1);
  });

  it('should return "Изменения сохранены" as success message', () => {
    expect(sut.successMessage).toBe('Изменения сохранены');
  });

  it('should return empty form model as model', () => {
    expect(sut.model).toBe(testGame1);
  });
});
