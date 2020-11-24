import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  constructor() {}

  public getConstraints(): Observable<any> {
    return of(true);
  }

  public isNicknameUnique(): Observable<boolean> {
    return of(true);
  }
}
