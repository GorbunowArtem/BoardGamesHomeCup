import { Observable } from 'rxjs';
import { Player } from '../../models/player';

export interface IPlayerPersistenceStrategy {
  save(player: Player): Observable<void>;
  readonly successMessage: string;
  readonly player: Player;
}
