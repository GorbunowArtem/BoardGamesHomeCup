import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { GamesService } from 'src/app/games/games.service';
import { Game } from 'src/app/games/models/game';
import { SearchGamesOptions } from 'src/app/games/models/search-games-options';

@Component({
  selector: 'hc-add-play',
  templateUrl: './add-play.component.html',
  styleUrls: ['./add-play.component.scss']
})
export class AddPlayComponent implements OnInit {
  public gameAndDate: FormGroup;
  public players: FormGroup;
  public playNotes: FormGroup;

  public games: Game[] = [];

  myControl = new FormControl();
  constructor(private _fb: FormBuilder, private _gamesService: GamesService) {
    this.gameAndDate = _fb.group({
      game: [''],
      playedDate: ['']
    });

    this.players = _fb.group({
      playerScores: []
    });

    this.playNotes = _fb.group({
      notes: ['']
    });
  }

  ngOnInit() {
    this._gamesService
      .search(new SearchGamesOptions())
      .subscribe((val) => (this.games = val.items));
  }

  public save() {
    console.log(this.gameAndDate.value);
  }
  showGame(game: Game) {
    return game?.title;
  }
}
