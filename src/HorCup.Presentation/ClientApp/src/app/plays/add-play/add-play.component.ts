import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonService } from 'src/app/common/common.service';

@Component({
  selector: 'hc-add-play',
  templateUrl: './add-play.component.html',
  styleUrls: ['./add-play.component.scss']
})
export class AddPlayComponent implements OnInit {
  public gameAndDate: FormGroup;
  public players: FormGroup;
  public playNotes: FormGroup;

  constructor(private _fb: FormBuilder) {
    this.gameAndDate = _fb.group({
      gameId: [''],
      playedDate: ['']
    });

    this.players = _fb.group({
      playerScores: []
    });

    this.playNotes = _fb.group({
      notes: ['']
    });
  }

  ngOnInit() {}

  public save() {}
}
