import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PlayersService } from '../players.service';

@Component({
  selector: 'hc-add-edit-player-dialog',
  templateUrl: './add-edit-player-dialog.component.html',
  styleUrls: ['./add-edit-player-dialog.component.scss']
})
export class AddEditPlayerDialogComponent implements OnInit {
  public playersForm!: FormGroup;

  constructor(private _fb: FormBuilder, private _playersService: PlayersService) {}

  ngOnInit() {
    this._playersService.getConstraints().subscribe((constr) => {
      this.playersForm = this._fb.group({
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        nickname: ['', [Validators.required]],
        birthDate: ['', [Validators.required]]
      });
    });
  }

  public onSubmit() {}

  public cancel() {}
}
