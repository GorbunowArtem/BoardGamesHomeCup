import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IFormPersistenceStrategy } from 'src/app/common/models/form-persistence-strategy';
import {
  max,
  NotUnique,
  RequiredField
} from 'src/app/common/validation-messages/validation-messages';
import { GamesService } from '../games.service';
import { Game } from '../models/game';
import { GamesPersistenceFactory } from './games-strategy/games-persistence-factory';

@Component({
  selector: 'hc-add-edit-game-dialog',
  templateUrl: './add-edit-game-dialog.component.html',
  styleUrls: ['./add-edit-game-dialog.component.scss']
})
export class AddEditGameDialogComponent implements OnInit {
  public gameForm!: FormGroup;

  public playersNumberOptions: number[] = [];

  public errorMessages!: any;

  private readonly _strategy: IFormPersistenceStrategy<Game>;

  public constructor(
    private _dialogRef: MatDialogRef<AddEditGameDialogComponent>,
    private _fb: FormBuilder,
    private _gamesService: GamesService,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) game: Game
  ) {
    this._strategy = new GamesPersistenceFactory().getStrategy(game, _gamesService);
    this.populatePlayersNumberOptions();
  }

  public ngOnInit() {
    const constraints = this._gamesService.constraints;

    const model = this._strategy.model;

    this.gameForm = this._fb.group({
      id: [model.id],
      title: [
        model.title,
        { updateOn: 'blur' },
        [Validators.required, Validators.maxLength(constraints.titleMaxLength)]
      ],
      minPlayers: [model.minPlayers, [Validators.required, Validators.max(constraints.minPlayers)]],
      maxPlayers: [model.maxPlayers, [Validators.required, Validators.max(constraints.maxPlayers)]]
    });

    this.errorMessages = {
      title: [RequiredField, max(constraints.titleMaxLength), NotUnique],
      minPlayers: [RequiredField, max(constraints.minPlayers)],
      maxPlayers: [RequiredField, max(constraints.maxPlayers)]
    };
  }

  public cancel() {
    this._dialogRef.close();
  }

  public save() {
    this._strategy.save(this.gameForm.value).subscribe(() => {
      this._dialogRef.close();
      this._snackBar.open(this._strategy.successMessage, undefined, {
        duration: 2000
      });
    });
  }

  private populatePlayersNumberOptions() {
    for (let i = 2; i <= 24; i++) {
      this.playersNumberOptions.push(i);
    }
  }
}
