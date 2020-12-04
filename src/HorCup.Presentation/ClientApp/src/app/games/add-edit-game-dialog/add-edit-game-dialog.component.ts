import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'hc-add-edit-game-dialog',
  templateUrl: './add-edit-game-dialog.component.html',
  styleUrls: ['./add-edit-game-dialog.component.scss']
})
export class AddEditGameDialogComponent implements OnInit {
  public gameForm: FormGroup;

  public playersNumberOptions: number[] = [];

  constructor(
    private _dialogRef: MatDialogRef<AddEditGameDialogComponent>,
    private _fb: FormBuilder
  ) {
    this.populatePlayersNumberOptions();
    this.gameForm = _fb.group({
      title: [''],
      minPlayers: [''],
      maxPlayers: ['']
    });
  }

  ngOnInit() {}

  public cancel() {
    this._dialogRef.close();
  }

  public save() {
    this._dialogRef.close();
  }

  private populatePlayersNumberOptions() {
    for (let i = 2; i <= 12; i++) {
      this.playersNumberOptions.push(i);
    }
  }
}
