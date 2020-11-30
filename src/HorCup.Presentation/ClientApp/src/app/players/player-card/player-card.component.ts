import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/common/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogModel } from 'src/app/common/models/confirmation-dialog-model';
import { Player } from '../models/player';
import { PlayersService } from '../players.service';

@Component({
  selector: 'hc-player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.scss']
})
export class PlayerCardComponent {
  @Input()
  player!: Player;

  constructor(public dialog: MatDialog, private _playersService: PlayersService) {}

  get fullName() {
    return `${this.player.firstName} ${this.player.lastName}`;
  }

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
}
