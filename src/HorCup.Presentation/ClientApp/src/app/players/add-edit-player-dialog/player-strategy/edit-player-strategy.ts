import { Observable } from 'rxjs';
import { Player } from '../../models/player';
import { PlayersService } from '../../players.service';
import { IPlayerPersistenceStrategy } from './i-player-persistence-strategy';

export class EditPlayerStrategy implements IPlayerPersistenceStrategy {
  public constructor(private _player: Player, private _playersService: PlayersService) {}

  public save(player: Player): Observable<void> {
    return this._playersService.edit(player);
  }

  public get successMessage(): string {
    return 'Изменения сохранены';
  }

  public get player(): Player {
    return this._player;
  }
}
