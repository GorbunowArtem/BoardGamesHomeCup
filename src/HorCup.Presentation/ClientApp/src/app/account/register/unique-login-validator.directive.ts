import { Directive } from '@angular/core';
import {
  AbstractControl,
  AsyncValidator,
  NG_ASYNC_VALIDATORS,
  ValidationErrors
} from '@angular/forms';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthService } from 'src/app/common/auth.service';

@Directive({
  selector: '[hcUniqueLoginValidator]',
  providers: [
    { provide: NG_ASYNC_VALIDATORS, useExisting: UniqueLoginValidatorDirective, multi: true }
  ]
})
export class UniqueLoginValidatorDirective implements AsyncValidator {
  public constructor(private _authService: AuthService) {}

  public validate(
    control: AbstractControl
  ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
    return this._authService.isLoginUnique(control.value).pipe(
      map(() => null),
      catchError(() => of({ notUnique: true }))
    );
  }
}
