import { Component, Input, OnInit } from '@angular/core';
import { Game } from '../models/game';

@Component({
  selector: 'hc-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent {
  @Input()
  public game!: Game;

  public rate = 5;
}
