import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CommonService } from 'src/app/common/common.service';
import { max, RequiredField } from 'src/app/common/validation-messages/validation-messages';
import { GamesService } from '../games.service';

@Component({
  selector: 'hc-add-edit-game-dialog',
  templateUrl: './add-edit-game-dialog.component.html',
  styleUrls: ['./add-edit-game-dialog.component.scss']
})
export class AddEditGameDialogComponent implements OnInit {
  public gameForm!: FormGroup;

  public playersNumberOptions: number[] = [];

  public errorMessages!: any;

  public constructor(
    private _dialogRef: MatDialogRef<AddEditGameDialogComponent>,
    private _fb: FormBuilder,
    private _gamesService: GamesService,
    private _commonService: CommonService
  ) {
    this.populatePlayersNumberOptions();
  }

  public ngOnInit() {
    const constraints = this._commonService.constraints.gamesConstraints;
    this.gameForm = this._fb.group({
      title: ['', [Validators.required, Validators.maxLength(constraints.titleMaxLength)]],
      minPlayers: ['', [Validators.required, Validators.max(constraints.minPlayers)]],
      maxPlayers: ['', [Validators.required, Validators.max(constraints.maxPlayers)]]
    });

    this.errorMessages = {
      title: [RequiredField, max(constraints.titleMaxLength)],
      minPlayers: [RequiredField, max(constraints.minPlayers)],
      maxPlayers: [RequiredField, max(constraints.maxPlayers)]
    };
  }

  public cancel() {
    this._dialogRef.close();
  }

  public save() {
    this._gamesService.add(this.gameForm.value).subscribe(() => {
      this._dialogRef.close();
    });
  }

  private populatePlayersNumberOptions() {
    for (let i = 2; i <= 24; i++) {
      this.playersNumberOptions.push(i);
    }
  }
}
