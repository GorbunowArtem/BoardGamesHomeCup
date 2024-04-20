import { Play } from '../models/play';

export const testPlay1: Play = {
  gameId: 'gameId',
  gameTitle: 'test game 1',
  id: 'playId',
  notes: 'Test play note 1',
  playedDate: new Date(2020, 2, 2),
  playerScores: {
    $values: [
      {
        isWinner: true,
        player: {
          id: 'player1Id',
          name: 'Player 1'
        },
        score: 22
      },
      {
        isWinner: false,
        player: {
          id: 'player2Id',
          name: 'Player 2'
        },
        score: 11
      }
    ]
  }
};
