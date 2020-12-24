import { AddPlayerStrategy } from './add-player-strategy';
import { EditPlayerStrategy } from './edit-player-strategy';
import { PlayersPersistenceFactory } from './players-persistence-factory';

describe('Players persistence factory', () => {
  it('should return AddPlayerStrategy if model is null', () => {
    const strategy = new PlayersPersistenceFactory().getStrategy(null, null as any);

    expect(strategy instanceof AddPlayerStrategy).toBeTruthy();
  });

  it('should return EditPlayerStrategy if model is not null', () => {
    const strategy = new PlayersPersistenceFactory().getStrategy({} as any, null as any);

    expect(strategy instanceof EditPlayerStrategy).toBeTruthy();
  });
});
