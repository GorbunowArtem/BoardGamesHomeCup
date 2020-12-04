import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { GamesService } from '../games.service';
import { SearchGamesOptions } from '../models/search-games-options';

@Component({
  selector: 'hc-games-filter',
  templateUrl: './games-filter.component.html',
  styleUrls: ['./games-filter.component.scss']
})
export class GamesFilterComponent {
  public playersNumberOptions: number[] = [];

  public gamesFilter: FormGroup;

  constructor(
    private _fb: FormBuilder,
    private _gamesService: GamesService,
    private _filterRef: MatBottomSheetRef,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: SearchGamesOptions
  ) {
    this.populatePlayersNumberOptions();
    this.gamesFilter = this._fb.group({
      minPlayers: [data.minPlayers],
      maxPlayers: [data.maxPlayers],
      searchText: [data.searchText]
    });
  }

  public search() {
    this._gamesService.searchParamsChangedSubject.next(this.gamesFilter.value);
    this._filterRef.dismiss();
  }

  public reset() {
    this.gamesFilter = this.defaultForm;
  }

  private get defaultForm() {
    return this._fb.group({
      minPlayers: [''],
      maxPlayers: [''],
      searchText: ['']
    });
  }

  private populatePlayersNumberOptions() {
    for (let i = 2; i <= 12; i++) {
      this.playersNumberOptions.push(i);
    }
  }
}
