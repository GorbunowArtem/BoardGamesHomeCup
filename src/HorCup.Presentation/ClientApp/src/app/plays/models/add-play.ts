import { IdName } from 'src/app/common/models/id-name';
import { PlayScore } from './play-score';

export interface AddPlay {
  game: IdName;
  notes: string;
  playerScores: PlayScore[];
  playedDate: Date;
}
