import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { PlayersService } from 'src/app/players/players.service';
import { Player } from 'src/app/players/models/player';
import { SearchPlayersOptions } from 'src/app/players/models/search-players-options';
import { GamesService } from 'src/app/games/games.service';
import { Game } from 'src/app/games/models/game';
import { SearchGamesOptions } from 'src/app/games/models/search-games-options';

@Component({
  selector: 'hc-plays-filter',
  templateUrl: './plays-filter.component.html',
  styleUrls: ['./plays-filter.component.scss']
})
export class PlaysFilterComponent implements OnInit {
  public playsFilter: FormGroup;

  public playersCtrl = new FormControl();
  public gamesCtrl = new FormControl();

  public selectedPlayers: Set<Player> = new Set<Player>();
  public selectedGames: Set<Game> = new Set<Game>();

  public playersOption!: Observable<Player[]>;
  public gamesOption!: Observable<Game[]>;

  public constructor(
    private _fb: FormBuilder,
    private _playersService: PlayersService,
    private _gamesService: GamesService
  ) {
    this.playsFilter = this._fb.group({
      playersIds: [''],
      gamesIds: [''],
      dateFrom: [''],
      dateTo: ['']
    });
  }

  public ngOnInit() {
    this.playersOption = this.playersCtrl.valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterPlayers(searchText))
    );

    this.gamesOption = this.gamesCtrl.valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterGames(searchText))
    );
  }

  public reset() {}
  public search() {
    console.log(this.playersCtrl.value);
    console.log(this.selectedPlayers);
    console.log(this.playsFilter.value);
  }

  public removePlayer(player: Player): void {
    this.selectedPlayers.delete(player);
  }

  public removeGame(game: Game): void {
    this.selectedGames.delete(game);
  }

  public onPlayerSelected(event: MatAutocompleteSelectedEvent): void {
    this.selectedPlayers.add(event.option.value);
    this.playersCtrl.setValue(null);
  }

  public onGameSelected(event: MatAutocompleteSelectedEvent): void {
    this.selectedGames.add(event.option.value);
    this.gamesCtrl.setValue(null);
  }

  public displayPlayer(player: Player) {
    return player ? `${player?.firstName} ${player?.lastName}` : '';
  }

  public get selectedPlayersIds(): string[] {
    const ids: any[] = [];

    this.selectedPlayers.forEach((p: Player) => {
      ids.push(p.id);
    });

    return ids;
  }

  public get selectedGamesIds(): string[] {
    const ids: any[] = [];

    this.selectedGames.forEach((g: Game) => {
      ids.push(g.id);
    });

    return ids;
  }

  private filterPlayers(searchText: string | Player): Observable<Player[]> {
    if (typeof searchText !== 'string') {
      searchText = '';
    }
    return this._playersService
      .search(new SearchPlayersOptions(10, 0, searchText, this.selectedPlayersIds))
      .pipe(map((resp) => resp.items));
  }

  private filterGames(searchText: string | Player): Observable<Game[]> {
    if (typeof searchText !== 'string') {
      searchText = '';
    }
    return this._gamesService
      .search(new SearchGamesOptions(10, 0, searchText, 0, 50, this.selectedGamesIds))
      .pipe(map((resp) => resp.items));
  }
}
