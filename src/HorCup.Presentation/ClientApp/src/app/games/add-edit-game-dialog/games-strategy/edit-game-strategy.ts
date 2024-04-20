import { Observable } from 'rxjs';
import { IFormPersistenceStrategy } from 'src/app/common/models/form-persistence-strategy';
import { GamesService } from '../../games.service';
import { Game } from '../../models/game';

export class EditGameStrategy implements IFormPersistenceStrategy<Game> {
  public constructor(private _model: Game, private _service: GamesService) {}

  public save(model: Game): Observable<void> {
    return this._service.edit(model);
  }

  public get successMessage(): string {
    return 'Изменения сохранены';
  }

  public get model(): Game {
    return this._model;
  }
}
