import { Observable } from 'rxjs';
import { IFormPersistenceStrategy } from 'src/app/common/models/i-form-persistence-strategy';
import { GamesService } from '../../games.service';
import { Game } from '../../models/game';

export class AddGameStrategy implements IFormPersistenceStrategy<Game> {
  public constructor(private _gamesService: GamesService) {}

  public save(model: Game): Observable<void> {
    return this._gamesService.add(model);
  }

  public get successMessage(): string {
    return 'Игра добавлена';
  }

  public get model(): Game {
    return {
      id: undefined,
      maxPlayers: '',
      minPlayers: '',
      title: ''
    };
  }
}
