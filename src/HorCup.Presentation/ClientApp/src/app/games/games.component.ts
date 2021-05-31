import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageableDataSource } from '../common/pageable-data-source/pageable-data-source';
import { AddEditGameDialogComponent } from './add-edit-game-dialog/add-edit-game-dialog.component';
import { GamesService } from './games.service';
import { SearchGamesOptions } from './models/search-games-options';

@Component({
  selector: 'hc-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent {
  public games: PageableDataSource;

  public constructor(gamesService: GamesService, private _addEditGameDialog: MatDialog) {
    this.games = new PageableDataSource(gamesService, new SearchGamesOptions());
    gamesService.init();
  }

  public openAddDialog() {
    this._addEditGameDialog.open(AddEditGameDialogComponent, {
      disableClose: true
    });
  }
}
