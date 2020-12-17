import { Component, Input } from '@angular/core';
import { Play } from '../models/play';

@Component({
  selector: 'hc-play-panel',
  templateUrl: './play-panel.component.html',
  styleUrls: ['./play-panel.component.scss']
})
export class PlayPanelComponent {
  @Input()
  play!: Play;

  displayedColumns: string[] = ['player-name', 'score'];

  get winnerName() {
    const winner = this.play.playerScores.find((pl) => {
      return pl.isWinner;
    });

    return winner?.player.name;
  }
}
