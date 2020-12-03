import { Component, Input, OnInit } from '@angular/core';
import { Game } from '../models/game';

@Component({
  selector: 'hc-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.scss']
})
export class GameCardComponent implements OnInit {
  @Input()
  game!: Game;

  constructor() {}

  rate: number = 1;

  ngOnInit() {}
}
