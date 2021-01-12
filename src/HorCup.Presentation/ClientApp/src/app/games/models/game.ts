export interface Game {
  id: string | undefined;
  title: string;
  maxPlayers: number | string;
  minPlayers: number | string;
  timesPlayed?: number;
}
