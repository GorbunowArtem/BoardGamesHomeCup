import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AddEditPlayerDialogComponent } from './add-edit-player-dialog/add-edit-player-dialog.component';

@Component({
  selector: 'hc-players',
  templateUrl: './players.component.html',
  styleUrls: ['./players.component.scss']
})
export class PlayersComponent implements OnInit {
  public constructor(private _dialog: MatDialog) {}

  public showDialog() {
    this._dialog.open(AddEditPlayerDialogComponent, {
      disableClose: true
    });
  }
  ngOnInit() {}
}
