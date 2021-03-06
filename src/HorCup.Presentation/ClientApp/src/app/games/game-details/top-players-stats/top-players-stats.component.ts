import { Component, Input } from '@angular/core';
import { PlayerStatistic } from 'src/app/players/models/player-statistic';

@Component({
  selector: 'hc-top-players-stats',
  templateUrl: './top-players-stats.component.html',
  styleUrls: ['./top-players-stats.component.scss']
})
export class TopPlayersStatsComponent {
  @Input()
  public playersStats: PlayerStatistic[];

  @Input()
  public title: string;

  @Input()
  public propertyName: string;

  public displayedColumns: string[] = ['player-name', 'stat'];

  public constructor() {
    this.playersStats = [];
    this.title = '';
    this.propertyName = '';
  }
  public getValue(player: PlayerStatistic) {
    return (player as any)[this.propertyName];
  }
}
