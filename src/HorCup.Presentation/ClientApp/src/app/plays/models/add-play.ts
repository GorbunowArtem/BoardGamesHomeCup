import { PlayScore } from './play-score';

export interface AddPlay {
  gameId: string;
  notes: string;
  playerScores: PlayScore[];
  playedDate: string;
}
