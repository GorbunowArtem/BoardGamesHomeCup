import { Directive } from '@angular/core';
import {
  AbstractControl,
  AsyncValidator,
  NG_ASYNC_VALIDATORS,
  ValidationErrors
} from '@angular/forms';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { GamesService } from '../games.service';

@Directive({
  selector: '[hcUniqueGameTitleValidator]',
  providers: [{ provide: NG_ASYNC_VALIDATORS, useExisting: UniqueGameTitleValidator, multi: true }]
})
export class UniqueGameTitleValidator implements AsyncValidator {
  public constructor(private _gamesService: GamesService) {}

  public validate(
    control: AbstractControl
  ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
    return this._gamesService
      .isTitleUnique(
        control.value,
        control.parent?.get('id')?.value === null ? '' : control.parent?.get('id')?.value
      )
      .pipe(
        map(() => null),
        catchError(() => of({ notUnique: true }))
      );
  }
}
