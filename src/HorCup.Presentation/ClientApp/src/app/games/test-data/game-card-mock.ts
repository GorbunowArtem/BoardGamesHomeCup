import { Component, Input } from '@angular/core';
import { Game } from '../models/game';

@Component({
  selector: 'hc-game-card',
  template: `<div>Card</div>`
})
export class GameCardMockComponent {
  @Input()
  public game!: Game;
}
