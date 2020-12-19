import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { CommonService } from 'src/app/common/common.service';
import {
  maxLength,
  NotUnique,
  RequiredField
} from 'src/app/common/validation-messages/validation-messages';
import { PlayersService } from '../players.service';

@Component({
  selector: 'hc-add-edit-player-dialog',
  templateUrl: './add-edit-player-dialog.component.html',
  styleUrls: ['./add-edit-player-dialog.component.scss']
})
export class AddEditPlayerDialogComponent implements OnInit {
  public playersForm!: FormGroup;

  public errorMessages!: any;

  public constructor(
    private _fb: FormBuilder,
    private _playersService: PlayersService,
    private _dialogRef: MatDialogRef<AddEditPlayerDialogComponent>,
    private _commonService: CommonService
  ) {}

  public ngOnInit() {
    const constr = this._commonService.constraints.playerConstraints;
    this.playersForm = this._fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(constr.maxNameLength)]],
      lastName: ['', [Validators.required, Validators.maxLength(constr.maxNameLength)]],
      nickname: [
        '',
        { updateOn: 'blur' },
        [Validators.required, Validators.maxLength(constr.maxNameLength)]
      ],
      birthDate: ['', [Validators.required]]
    });

    this.errorMessages = {
      firstName: [RequiredField, maxLength(constr.maxNameLength)],
      lastName: [RequiredField, maxLength(constr.maxNameLength)],
      nickname: [RequiredField, maxLength(constr.maxNameLength), NotUnique],
      birthDate: [RequiredField]
    };
  }

  public add() {
    this._playersService.add(this.playersForm.value).subscribe(() => {
      this._dialogRef.close();
    });
  }
}
