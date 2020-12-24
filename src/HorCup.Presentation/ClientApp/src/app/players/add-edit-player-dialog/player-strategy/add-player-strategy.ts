import { Observable } from 'rxjs';
import { Player } from '../../models/player';
import { PlayersService } from '../../players.service';
import { IPlayerPersistenceStrategy } from './i-player-persistence-strategy';

export class AddPlayerStrategy implements IPlayerPersistenceStrategy {
  public constructor(private _playersService: PlayersService) {}

  public save(player: Player): Observable<void> {
    return this._playersService.add(player);
  }
  public get successMessage(): string {
    return 'Игрок добавлен';
  }

  public get player(): Player {
    return {
      birthDate: '',
      firstName: '',
      lastName: '',
      nickname: '',
      id: undefined
    };
  }
}
