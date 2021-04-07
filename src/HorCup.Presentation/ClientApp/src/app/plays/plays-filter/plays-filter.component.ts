import { Component, OnInit } from '@angular/core';
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
import { PlaysService } from '../plays.service';
import { SearchPlaysOptions } from '../models/search-plays-options';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';

@Component({
  selector: 'hc-plays-filter',
  templateUrl: './plays-filter.component.html',
  styleUrls: ['./plays-filter.component.scss']
})
export class PlaysFilterComponent implements OnInit {
  public playsFilter: FormGroup;
  private searchPlayerOptions: SearchPlayersOptions;

  public selectedPlayers: Set<Player> = new Set<Player>();
  public selectedGames: Set<Game> = new Set<Game>();

  public playersOption!: Observable<Player[]>;
  public gamesOption!: Observable<Game[]>;

  public constructor(
    private _fb: FormBuilder,
    private _playersService: PlayersService,
    private _gamesService: GamesService,
    private _playsService: PlaysService,
    private _filterRef: MatBottomSheetRef
  ) {
    this.playsFilter = this._fb.group({
      playersIds: [''],
      gamesIds: [''],
      dateFrom: [''],
      dateTo: ['']
    });

    this.searchPlayerOptions = new SearchPlayersOptions();
  }

  public ngOnInit() {
    this.playersOption = (this.playsFilter.get('playersIds') as FormControl).valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterPlayers(searchText))
    );

    this.gamesOption = (this.playsFilter.get('gamesIds') as FormControl).valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap((searchText) => this.filterGames(searchText))
    );
  }

  public reset() {
    this.playsFilter = this._fb.group({
      playersIds: [''],
      gamesIds: [''],
      dateFrom: [''],
      dateTo: ['']
    });
    this.selectedPlayers = new Set<Player>();
    this.selectedGames = new Set<Game>();
  }

  public search() {
    let searchOptions = new SearchPlaysOptions();
    (searchOptions.skip = 0),
      (searchOptions.take = 10),
      (searchOptions.gamesIds = this.selectedGamesIds),
      (searchOptions.playersIds = this.selectedPlayersIds),
      (searchOptions.dateFrom = this.getFormDate('dateFrom')),
      (searchOptions.dateTo = this.getFormDate('dateTo'));

    this._playsService.searchParamsChangedSubject.next(searchOptions);
    this._filterRef.dismiss();
  }

  public removePlayer(player: Player): void {
    this.selectedPlayers.delete(player);
  }

  public removeGame(game: Game): void {
    this.selectedGames.delete(game);
  }

  public onPlayerSelected(event: MatAutocompleteSelectedEvent): void {
    this.selectedPlayers.add(event.option.value);
  }

  public onGameSelected(event: MatAutocompleteSelectedEvent): void {
    this.selectedGames.add(event.option.value);
  }

  public displayPlayer(player: Player) {
    return player ? `${player?.nickname}` : '';
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

  private getFormDate(fieldName: string): string {
    return this.playsFilter.get(fieldName)?.value
      ? this.playsFilter.get(fieldName)?.value.toISOString()
      : '';
  }

  private filterPlayers(searchText: string | Player): Observable<Player[]> {
    if (typeof searchText !== 'string') {
      this.searchPlayerOptions.searchText = '';
    } else {
      this.searchPlayerOptions.searchText = searchText;
    }

    this.searchPlayerOptions.exceptIds = this.selectedPlayersIds;

    return this._playersService
      .search(this.searchPlayerOptions)
      .pipe(map((resp) => resp.items.$values));
  }

  private filterGames(searchText: string | Player): Observable<Game[]> {
    if (typeof searchText !== 'string') {
      searchText = '';
    }
    return this._gamesService
      .search(new SearchGamesOptions(10, 0, searchText, 1, 24, this.selectedGamesIds))
      .pipe(map((resp) => resp.items.$values));
  }
}
