import { Play } from '../models/play';

export const testPlay1: Play = {
  gameId: 'gameId',
  gameTitle: 'test game 1',
  id: 'playId',
  notes: 'Test play note 1',
  playedDate: new Date(),
  playerScores: [
    {
      isWinner: true,
      player: {
        id: 'player1Id',
        name: 'Player 1'
      },
      score: 22
    }
  ]
};
