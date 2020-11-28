import { Component, Input, OnInit } from '@angular/core';
import { Player } from '../models/player';

@Component({
  selector: 'hc-player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.scss']
})
export class PlayerCardComponent {
  @Input()
  player!: Player;

  constructor() {}

  get fullName() {
    return `${this.player.firstName} ${this.player.lastName}`;
  }
}
