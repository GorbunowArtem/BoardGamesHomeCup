import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { GamesService } from 'src/app/games/games.service';
import { Game } from 'src/app/games/models/game';
import { SearchGamesOptions } from 'src/app/games/models/search-games-options';
import { Player } from 'src/app/players/models/player';
import { SearchPlayersOptions } from 'src/app/players/models/search-players-options';
import { PlayersService } from 'src/app/players/players.service';
import { AddPlay } from '../models/add-play';
import { PlaysService } from '../plays.service';

@Component({
  selector: 'hc-add-play',
  templateUrl: './add-play.component.html',
  styleUrls: ['./add-play.component.scss']
})
export class AddPlayComponent implements OnInit {
  public addPlayForm = this._fb.group({
    notes: [''],
    selectedGame: [null, [Validators.required]],
    playedDate: [new Date(), [Validators.required]],
    playerScores: this._fb.array([
      this._fb.group({
        player: [null, [Validators.required]],
        score: ['', Validators.required]
      })
    ])
  });

  public gamesOptions!: Observable<Game[]>;
  public playersOption!: Observable<Player[]>;

  constructor(
    private _fb: FormBuilder,
    private _gamesService: GamesService,
    private _playersService: PlayersService,
    private _playService: PlaysService
  ) {}

  ngOnInit() {
    this.gamesOptions = (this.addPlayForm.get('selectedGame') as FormControl).valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterGames(searchText))
    );

    this.playersOption = (this.playerScores.controls[0].get(
      'player'
    ) as FormControl).valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterPlayers(searchText))
    );
  }

  get selectedGame() {
    return this.addPlayForm.get('selectedGame')?.value;
  }

  get playerScores() {
    return this.addPlayForm.get('playerScores') as FormArray;
  }

  public addPlayer() {
    this.playerScores.push(
      this._fb.group({
        player: [null],
        score: ['']
      })
    );

    const index = this.playerScores.controls.length - 1;
    this.playersOption = (this.playerScores.controls[index].get(
      'player'
    ) as FormControl).valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterPlayers(searchText))
    );
  }

  public removePlayer() {
    if (this.playerScores.length > 1) {
      this.playerScores.removeAt(this.playerScores.length - 1);
    }
  }

  public canAddPlayer(): boolean {
    if (this.selectedGame === null) {
      return true;
    }

    return this.selectedGame.maxPlayers >= this.playerScores.length;
  }

  public displayPlayer(player: Player) {
    return player ? `${player?.firstName} ${player?.lastName}` : '';
  }

  public displayGame(game: Game) {
    return game?.title;
  }

  private filterGames(searchText: string | Game) {
    if (typeof searchText !== 'string') {
      searchText = '';
    }

    return this._gamesService
      .search(new SearchGamesOptions(10, 0, searchText))
      .pipe(map((resp) => resp.items));
  }

  private getSelectedPlayersIds(): string[] {
    if (this.playerScores) {
      return this.playerScores.value
        .filter((p: any) => p.player !== null)
        .map((p: any) => p.player.id);
    }

    return [];
  }

  private filterPlayers(searchText: string | Player): Observable<Player[]> {
    if (typeof searchText !== 'string') {
      searchText = '';
    }
    return this._playersService
      .search(new SearchPlayersOptions(10, 0, searchText, this.getSelectedPlayersIds()))
      .pipe(map((resp) => resp.items));
  }

  public save() {
    this._playService.add(this.getModel()).subscribe();
  }

  private getModel(): AddPlay {
    return {
      gameId: this.selectedGame.id,
      notes: this.addPlayForm.get('notes')?.value,
      playedDate: this.addPlayForm.get('playedDate')?.value,
      playerScores: this.playerScores.value.map((p: any) => {
        return {
          player: {
            id: p.player.id
          },
          score: p.score
        };
      })
    };
  }
}
