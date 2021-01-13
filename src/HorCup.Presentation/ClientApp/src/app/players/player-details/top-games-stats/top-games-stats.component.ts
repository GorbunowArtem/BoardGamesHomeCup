import { Component, Input } from '@angular/core';
import { PlayerStatistic } from '../../models/player-statistic';

@Component({
  selector: 'hc-top-games-stats',
  templateUrl: './top-games-stats.component.html',
  styleUrls: ['./top-games-stats.component.scss']
})
export class TopGamesStatsComponent {
  @Input()
  public playersStats: PlayerStatistic[];

  @Input()
  public title: string;

  @Input()
  public propertyName: string;

  public displayedColumns: string[] = ['game-title', 'stat'];

  public constructor() {
    this.playersStats = [];
    this.title = '';
    this.propertyName = '';
  }
  public getValue(player: PlayerStatistic) {
    return (player as any)[this.propertyName];
  }
}
