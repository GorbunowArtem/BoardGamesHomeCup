import { Observable } from 'rxjs';
import { Player } from '../../models/player';
import { PlayersService } from '../../players.service';
import { IFormPersistenceStrategy } from '../../../common/models/i-form-persistence-strategy';

export class AddPlayerStrategy implements IFormPersistenceStrategy<Player> {
  public constructor(private _playersService: PlayersService) {}

  public save(player: Player): Observable<void> {
    return this._playersService.add(player);
  }
  public get successMessage(): string {
    return 'Игрок добавлен';
  }

  public get model(): Player {
    return {
      nickname: '',
      id: undefined
    };
  }
}
