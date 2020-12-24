import { Player } from '../../models/player';
import { PlayersService } from '../../players.service';
import { AddPlayerStrategy } from './add-player-strategy';
import { EditPlayerStrategy } from './edit-player-strategy';
import { IPlayerPersistenceStrategy } from './i-player-persistence-strategy';

export class PlayersPersistenceFactory {
  public getStrategy(player: Player, playersService: PlayersService): IPlayerPersistenceStrategy {
    if (player === null) {
      return new AddPlayerStrategy(playersService);
    } else {
      return new EditPlayerStrategy(player, playersService);
    }
  }
}
