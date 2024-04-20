import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/common/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogModel } from 'src/app/common/models/confirmation-dialog-model';
import { AddEditGameDialogComponent } from '../add-edit-game-dialog/add-edit-game-dialog.component';
import { GamesService } from '../games.service';
import { Game } from '../models/game';

@Component({
  selector: 'hc-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent {
  @Input()
  public game!: Game;

  public rate = 5;

  public constructor(public dialog: MatDialog, private _gamesService: GamesService) {}

  public delete() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      disableClose: true,
      data: new ConfirmationDialogModel()
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._gamesService.delete(this.game.id).subscribe();
      }
    });
  }

  public edit() {
    this.dialog.open(AddEditGameDialogComponent, {
      disableClose: true,
      data: this.game
    });
  }
}
