import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonService } from 'src/app/common/common.service';
import {
  maxLength,
  NotUnique,
  RequiredField
} from 'src/app/common/validation-messages/validation-messages';
import { Player } from '../models/player';
import { PlayersService } from '../players.service';
import { IPlayerPersistenceStrategy } from './player-strategy/i-player-persistence-strategy';
import { PlayersPersistenceFactory } from './player-strategy/players-persistence-factory';

@Component({
  selector: 'hc-add-edit-player-dialog',
  templateUrl: './add-edit-player-dialog.component.html',
  styleUrls: ['./add-edit-player-dialog.component.scss']
})
export class AddEditPlayerDialogComponent implements OnInit {
  public playersForm!: FormGroup;

  public errorMessages!: any;

  private readonly _playerStrategy: IPlayerPersistenceStrategy;

  public constructor(
    private _fb: FormBuilder,
    _playersService: PlayersService,
    private _dialogRef: MatDialogRef<AddEditPlayerDialogComponent>,
    private _commonService: CommonService,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) data: Player
  ) {
    this._playerStrategy = new PlayersPersistenceFactory().getStrategy(data, _playersService);
  }

  public ngOnInit() {
    const constr = this._commonService.constraints.playerConstraints;

    const model = this._playerStrategy.player;

    this.playersForm = this._fb.group({
      id: [model.id],
      firstName: [
        model.firstName,
        [Validators.required, Validators.maxLength(constr.maxNameLength)]
      ],
      lastName: [model.lastName, [Validators.required, Validators.maxLength(constr.maxNameLength)]],
      nickname: [
        model.nickname,
        { updateOn: 'blur' },
        [Validators.required, Validators.maxLength(constr.maxNameLength)]
      ],
      birthDate: [model.birthDate, [Validators.required]]
    });

    this.errorMessages = {
      firstName: [RequiredField, maxLength(constr.maxNameLength)],
      lastName: [RequiredField, maxLength(constr.maxNameLength)],
      nickname: [RequiredField, maxLength(constr.maxNameLength), NotUnique],
      birthDate: [RequiredField]
    };
  }

  public save() {
    this._playerStrategy.save(this.playersForm.value).subscribe(() => {
      this._dialogRef.close();
      this._snackBar.open(this._playerStrategy.successMessage, undefined, {
        duration: 2000
      });
    });
  }
}
