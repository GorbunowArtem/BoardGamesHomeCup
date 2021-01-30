import { Game } from './game';

export interface GameDetails extends Game {
  timesPlayed: number;
  averageScore: number | null;
  lastPlayedDate: string;
}
