import { Directive, Injectable } from '@angular/core';
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
  selector: '[uniqueNicknameValidator]',
  providers: [{ provide: NG_ASYNC_VALIDATORS, useExisting: UniqueNicknameValidator, multi: true }]
})
export class UniqueNicknameValidator implements AsyncValidator {
  constructor(private _playerService: PlayersService) {}

  validate(
    control: AbstractControl
  ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
    return this._playerService.isNicknameUnique(control.value).pipe(
      map(() => null),
      catchError(() => of({ notUnique: true }))
    );
  }
}
