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

@Component({
  selector: 'hc-add-edit-player-dialog',
  templateUrl: './add-edit-player-dialog.component.html',
  styleUrls: ['./add-edit-player-dialog.component.scss']
})
export class AddEditPlayerDialogComponent implements OnInit {
  public playersForm!: FormGroup;

  public errorMessages!: any;

  private playerModel: Player;

  private readonly _defaultPLayer: Player;

  private readonly _isEditMode: boolean;

  public constructor(
    private _fb: FormBuilder,
    private _playersService: PlayersService,
    private _dialogRef: MatDialogRef<AddEditPlayerDialogComponent>,
    private _commonService: CommonService,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) private data: Player
  ) {
    this._defaultPLayer = {
      birthDate: '',
      firstName: '',
      lastName: '',
      nickname: '',
      id: undefined
    };

    this._isEditMode = data !== null;
    this.playerModel = data ?? this._defaultPLayer;
  }

  public ngOnInit() {
    const constr = this._commonService.constraints.playerConstraints;

    this.playersForm = this._fb.group({
      id: [this.playerModel.id],
      firstName: [
        this.playerModel.firstName,
        [Validators.required, Validators.maxLength(constr.maxNameLength)]
      ],
      lastName: [
        this.playerModel.lastName,
        [Validators.required, Validators.maxLength(constr.maxNameLength)]
      ],
      nickname: [
        this.playerModel.nickname,
        { updateOn: 'blur' },
        [Validators.required, Validators.maxLength(constr.maxNameLength)]
      ],
      birthDate: [this.playerModel.birthDate, [Validators.required]]
    });

    this.errorMessages = {
      firstName: [RequiredField, maxLength(constr.maxNameLength)],
      lastName: [RequiredField, maxLength(constr.maxNameLength)],
      nickname: [RequiredField, maxLength(constr.maxNameLength), NotUnique],
      birthDate: [RequiredField]
    };
  }

  public save() {
    if (this._isEditMode) {
      this._playersService.edit(this.playersForm.value).subscribe(() => {
        this._dialogRef.close();
        this._snackBar.open('Игрок изменен', undefined, {
          duration: 2000
        });
      });
    } else {
      this._playersService.add(this.playersForm.value).subscribe(() => {
        this._dialogRef.close();
        this._snackBar.open('Игрок добавлен', undefined, {
          duration: 2000
        });
      });
    }
  }
}
