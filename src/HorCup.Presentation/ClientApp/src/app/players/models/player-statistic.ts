import { Player } from './player';

export interface PlayerStatistic {
  playerId: string;
  player: Player;
  gameId: string;
  playedTotal: number;
  wins: number;
  averageScore: number | null;
}
