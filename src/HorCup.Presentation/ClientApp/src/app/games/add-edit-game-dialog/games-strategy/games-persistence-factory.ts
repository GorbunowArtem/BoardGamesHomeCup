import { IFormPersistenceStrategy } from 'src/app/common/models/i-form-persistence-strategy';
import { GamesService } from '../../games.service';
import { Game } from '../../models/game';
import { AddGameStrategy } from './add-game-strategy';
import { EditGameStrategy } from './edit-game-strategy';

export class GamesPersistenceFactory {
  public getStrategy(
    game: Game | null,
    gamesService: GamesService
  ): IFormPersistenceStrategy<Game> {
    if (game === null) {
      return new AddGameStrategy(gamesService);
    } else {
      return new EditGameStrategy(game, gamesService);
    }
  }
}
