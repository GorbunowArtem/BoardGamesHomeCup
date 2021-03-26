import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/common/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogModel } from 'src/app/common/models/confirmation-dialog-model';
import { AddEditPlayerDialogComponent } from '../add-edit-player-dialog/add-edit-player-dialog.component';
import { Player } from '../models/player';
import { PlayersService } from '../players.service';

@Component({
  selector: 'hc-player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.scss']
})
export class PlayerCardComponent {
  @Input()
  public player!: Player;

  public constructor(public dialog: MatDialog, private _playersService: PlayersService) {}

  public delete() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      disableClose: true,
      data: new ConfirmationDialogModel()
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._playersService.delete(this.player.id).subscribe();
      }
    });
  }

  public edit() {
    this.dialog.open(AddEditPlayerDialogComponent, {
      data: this.player,
      disableClose: true
    });
  }
}
