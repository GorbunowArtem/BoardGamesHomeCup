import { PlayScore } from './play-score';

export interface Play {
  id: string;
  playerScores: PlayScore[];
  gameId: string;
  gameTitle: string;
  playedDate: string;
  notes: string;
}
