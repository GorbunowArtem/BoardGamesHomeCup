import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../common/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogModel } from '../common/models/confirmation-dialog-model';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';
import { Player } from './models/player';
import { PlayersService } from './players.service';
import { PageableDataSource } from '../common/pageable-data-source/pageable-data-source';
import { SearchPlayersOptions } from './models/search-players-options';

@Component({
  selector: 'hc-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlayersComponent {
  public playersSource;

  public constructor(private _dialog: MatDialog, private _playersService: PlayersService) {
    this.playersSource = new PageableDataSource(_playersService, new SearchPlayersOptions());
    this._playersService.init();
  }

  public addPlayer() {
    this._dialog.open(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  }

  public edit(player: Player) {
    this._dialog.open(AddEditPlayerDialogComponent, {
      data: player,
      disableClose: true
    });
  }

  public delete(id: string | undefined) {
    const dialogRef = this._dialog.open(ConfirmationDialogComponent, {
      disableClose: true,
      data: new ConfirmationDialogModel()
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this._playersService.delete(id).subscribe();
      }
    });
  }
}
