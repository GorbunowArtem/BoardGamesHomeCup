import { AddGameStrategy } from './add-game-strategy';
import { EditGameStrategy } from './edit-game-strategy';
import { GamesPersistenceFactory } from './games-persistence-factory';

describe('Games persistence factory', () => {
  it('should return AddGameStrategy if model is null', () => {
    const strategy = new GamesPersistenceFactory().getStrategy(null, null as any);

    expect(strategy instanceof AddGameStrategy).toBeTruthy();
  });

  it('should return EditGameStrategy if model is not null', () => {
    const strategy = new GamesPersistenceFactory().getStrategy({} as any, null as any);

    expect(strategy instanceof EditGameStrategy).toBeTruthy();
  });
});
