import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Player } from '../models/player';
import { PlayerDetails } from '../models/player-details';
import { PlayersService } from '../players.service';

@Component({
  selector: 'hc-player-details',
  templateUrl: './player-details.component.html',
  styleUrls: ['./player-details.component.scss']
})
export class PlayerDetailsComponent implements OnInit {
  @Input()
  player!: PlayerDetails;

  constructor(private _playersService: PlayersService, private _activatedRoute: ActivatedRoute) {}

  ngOnInit() {
    this._activatedRoute.paramMap.subscribe((params) => {
      const playerId = params.get('id');

      this._playersService.get(playerId).subscribe((player) => (this.player = player));
    });
  }
}
