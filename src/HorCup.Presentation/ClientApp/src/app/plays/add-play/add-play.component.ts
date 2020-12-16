import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { GamesService } from 'src/app/games/games.service';
import { Game } from 'src/app/games/models/game';
import { SearchGamesOptions } from 'src/app/games/models/search-games-options';
import { Player } from 'src/app/players/models/player';
import { SearchPlayersOptions } from 'src/app/players/models/search-players-options';
import { PlayersService } from 'src/app/players/players.service';

@Component({
  selector: 'hc-add-play',
  templateUrl: './add-play.component.html',
  styleUrls: ['./add-play.component.scss']
})
export class AddPlayComponent implements OnInit {
  public participants: FormGroup = this._fb.group({
    players: this._fb.array([this._fb.control('')])
  });
  public platNotes: FormGroup;

  public games!: Observable<Game[]>;
  public playersOption!: Observable<Player[]>;

  public selectedGame = new FormControl();
  public selectedDate = new FormControl();

  constructor(
    private _fb: FormBuilder,
    private _gamesService: GamesService,
    private _playersService: PlayersService
  ) {
    this.platNotes = _fb.group({
      notes: ['']
    });
  }

  ngOnInit() {
    this.games = this.selectedGame.valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterGames(searchText))
    );

    this.playersOption = this._playersService
      .search(new SearchPlayersOptions())
      .pipe(map((resp) => resp.items));
  }

  get players() {
    return this.participants.get('players') as FormArray;
  }

  addPlayer() {
    this.players.push(this._fb.control(''));
  }

  removePlayer() {
    if (this.players.length > 1) {
      this.players.removeAt(this.players.length - 1);
    }
  }

  public save() {
    console.log(this.selectedGame.value);
    console.log(this.platNotes.value);
    console.log(this.participants.value);
  }

  public displayGame(game: Game) {
    return game?.title;
  }

  public displayPlayer(player: Player) {
    return player ? `${player?.firstName} ${player?.lastName}` : '';
  }

  private filterGames(searchText: string) {
    return this._gamesService
      .search(new SearchGamesOptions(10, 0, searchText))
      .pipe(map((resp) => resp.items));
  }
}
