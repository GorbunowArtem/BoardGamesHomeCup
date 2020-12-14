import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { GamesService } from 'src/app/games/games.service';
import { Game } from 'src/app/games/models/game';
import { SearchGamesOptions } from 'src/app/games/models/search-games-options';

@Component({
  selector: 'hc-add-play',
  templateUrl: './add-play.component.html',
  styleUrls: ['./add-play.component.scss']
})
export class AddPlayComponent implements OnInit {
  public players: FormGroup;
  public playNotes: FormGroup;

  public games!: Observable<Game[]>;
  public selectedGame = new FormControl();
  public selectedDate = new FormControl();

  constructor(private _fb: FormBuilder, private _gamesService: GamesService) {
    this.players = _fb.group({
      playerScores: []
    });

    this.playNotes = _fb.group({
      notes: ['']
    });
  }

  ngOnInit() {
    this.games = this.selectedGame.valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterGames(searchText))
    );
  }

  public save() {
    console.log(this.selectedGame.value);
    console.log(this.selectedDate.value);
  }

  public displayGame(game: Game) {
    return game?.title;
  }

  private filterGames(searchText: string) {
    return this._gamesService
      .search(new SearchGamesOptions(10, 0, searchText))
      .pipe(map((resp) => resp.items));
  }
}
