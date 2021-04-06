import { Player } from '../../models/player';
import { PlayersService } from '../../players.service';
import { AddPlayerStrategy } from './add-player-strategy';
import { EditPlayerStrategy } from './edit-player-strategy';
import { IFormPersistenceStrategy } from '../../../common/models/i-form-persistence-strategy';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PlayersPersistenceFactory {
  public getStrategy(
    player: Player | null,
    playersService: PlayersService
  ): IFormPersistenceStrategy<Player> {
    if (player === null) {
      return new AddPlayerStrategy(playersService);
    } else {
      return new EditPlayerStrategy(player, playersService);
    }
  }
}
