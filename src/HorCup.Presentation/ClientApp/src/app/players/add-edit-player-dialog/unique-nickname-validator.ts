import { Directive } from '@angular/core';
import {
  AbstractControl,
  AsyncValidator,
  NG_ASYNC_VALIDATORS,
  ValidationErrors
} from '@angular/forms';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { PlayersService } from '../players.service';

@Directive({
  selector: '[hcUniqueNicknameValidator]',
  providers: [{ provide: NG_ASYNC_VALIDATORS, useExisting: UniqueNicknameValidator, multi: true }]
})
export class UniqueNicknameValidator implements AsyncValidator {
  public constructor(private _playerService: PlayersService) {}

  public validate(
    control: AbstractControl
  ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
    return this._playerService
      .isNicknameUnique(
        control.value,
        control.parent?.get('id')?.value === null ? '' : control.parent?.get('id')?.value
      )
      .pipe(
        map(() => null),
        catchError(() => of({ notUnique: true }))
      );
  }
}
