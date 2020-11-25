import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { PlayerViewModel } from './models/player';
import { PlayerConstraints } from './models/player-constraints';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {
  constructor(private _http: HttpClient) {}

  public getConstraints(): Observable<PlayerConstraints> {
    return this._http.get<PlayerConstraints>('/players/constraints');
  }

  public isNicknameUnique(nickname: string): Observable<any> {
    return this._http.head(`/players?nickname=${nickname}`, { observe: 'response' });
  }

  public addPlayer(player: PlayerViewModel) {
    return this._http.post('players', player);
  }
}
