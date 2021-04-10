import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PlayerSource } from '../players/players-source';
import { AddEditGameDialogComponent } from './add-edit-game-dialog/add-edit-game-dialog.component';
import { GamesService } from './games.service';

@Component({
  selector: 'hc-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss']
})
export class GamesComponent {
  public games: PlayerSource;

  public constructor(gamesService: GamesService, private _addEditGameDialog: MatDialog) {
    this.games = new PlayerSource(gamesService);
  }

  public openAddDialog() {
    this._addEditGameDialog.open(AddEditGameDialogComponent, {
      disableClose: true
    });
  }
}
