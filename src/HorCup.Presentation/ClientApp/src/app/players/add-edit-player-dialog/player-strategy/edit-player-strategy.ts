import { Observable } from 'rxjs';
import { Player } from '../../models/player';
import { PlayersService } from '../../players.service';
import { IFormPersistenceStrategy } from '../../../common/models/form-persistence-strategy';

export class EditPlayerStrategy implements IFormPersistenceStrategy<Player> {
  public constructor(private _player: Player, private _playersService: PlayersService) {}

  public save(player: Player): Observable<void> {
    return this._playersService.edit(player);
  }

  public get successMessage(): string {
    return 'Изменения сохранены';
  }

  public get model(): Player {
    return this._player;
  }
}
