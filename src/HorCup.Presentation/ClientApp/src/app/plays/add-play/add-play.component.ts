import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { GamesService } from 'src/app/games/games.service';
import { Game } from 'src/app/games/models/game';
import { SearchGamesOptions } from 'src/app/games/models/search-games-options';
import { Player } from 'src/app/players/models/player';
import { SearchPlayersOptions } from 'src/app/players/models/search-players-options';
import { PlayersService } from 'src/app/players/players.service';
import { AddPlay } from '../models/add-play';
import { PlayScore } from '../models/play-score';
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
    playerScores: this._fb.array([this.defaultPlayerScore, this.defaultPlayerScore])
  });

  public gamesOptions!: Observable<Game[]>;
  public playersOption!: Observable<Player[]>;
  private searchPlayersOptions;

  public constructor(
    private _fb: FormBuilder,
    private _gamesService: GamesService,
    private _playersService: PlayersService,
    private _playService: PlaysService,
    private _snackBar: MatSnackBar,
    private _router: Router
  ) {
    this.searchPlayersOptions = new SearchPlayersOptions();
  }

  public ngOnInit() {
    this.gamesOptions = (this.addPlayForm.get('selectedGame') as FormControl).valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterGames(searchText))
    );

    this.playersOption = (this.playerScoresControls[0].get(
      'player'
    ) as FormControl).valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterPlayers(searchText))
    );
  }

  public get defaultPlayerScore() {
    return this._fb.group({
      player: [null, [Validators.required]],
      score: ['', Validators.required]
    });
  }

  public get selectedGame() {
    return this.addPlayForm.get('selectedGame')?.value;
  }

  public get playerScores() {
    return this.addPlayForm.get('playerScores') as FormArray;
  }

  public get playerScoresControls() {
    return this.playerScores.controls as FormGroup[];
  }

  public addPlayer() {
    this.playerScores.push(this.defaultPlayerScore);
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
    return player ? `${player?.nickname}` : '';
  }

  public displayGame(game: Game) {
    return game?.title;
  }

  private filterGames(searchText: string | Game) {
    if (typeof searchText !== 'string') {
      searchText = '';
    }

    return this._gamesService
      .search(new SearchGamesOptions())
      .pipe(map((resp) => resp.items.$values));
  }

  private getSelectedPlayersIds(): string[] {
    if (this.playerScores) {
      return this.playerScores.value
        .filter((p: PlayScore) => p.player !== null)
        .map((p: PlayScore) => p.player.id);
    }

    return [];
  }

  private filterPlayers(searchText: string | Player): Observable<Player[]> {
    if (typeof searchText !== 'string') {
      this.searchPlayersOptions.searchText = '';
    } else {
      this.searchPlayersOptions.searchText = searchText;
    }

    this.searchPlayersOptions.exceptIds = this.getSelectedPlayersIds();

    return this._playersService
      .search(this.searchPlayersOptions)
      .pipe(map((resp) => resp.items.$values));
  }

  public save() {
    this._playService.add(this.getModel()).subscribe(() =>
      this._snackBar
        .open('Партия добавлена!', undefined, {
          duration: 2000
        })
        .afterDismissed()
        .subscribe(() => this._router.navigateByUrl('/'))
    );
  }

  private getModel(): AddPlay {
    return {
      game: this.selectedGame,
      notes: this.addPlayForm.get('notes')?.value,
      playedDate: this.addPlayForm.get('playedDate')?.value,
      playerScores: this.playerScores.value
    };
  }
}
