import { IdName } from 'src/app/common/models/id-name';

export interface PlayScore {
  player: IdName;
  score: number | null;
  isWinner: boolean;
}
