import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { PlayersService } from 'src/app/players/players.service';
import { Player } from 'src/app/players/models/player';
import { SearchPlayersOptions } from 'src/app/players/models/search-players-options';

@Component({
  selector: 'hc-plays-filter',
  templateUrl: './plays-filter.component.html',
  styleUrls: ['./plays-filter.component.scss']
})
export class PlaysFilterComponent implements OnInit {
  public playsFilter: FormGroup;
  public playersCtrl = new FormControl();
  public selectedPlayers: Set<Player> = new Set<Player>();
  public playersOption!: Observable<Player[]>;

  public constructor(private _fb: FormBuilder, private _playersService: PlayersService) {
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
  }

  public reset() {}
  public search() {
    console.log(this.playersCtrl.value);
    console.log(this.selectedPlayers);
  }

  public remove(player: Player): void {
    this.selectedPlayers.delete(player);
  }

  public selected(event: MatAutocompleteSelectedEvent): void {
    this.selectedPlayers.add(event.option.value);
    this.playersCtrl.setValue(null);
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

  private filterPlayers(searchText: string | Player): Observable<Player[]> {
    if (typeof searchText !== 'string') {
      searchText = '';
    }
    return this._playersService
      .search(new SearchPlayersOptions(10, 0, searchText, this.selectedPlayersIds))
      .pipe(map((resp) => resp.items));
  }
}
