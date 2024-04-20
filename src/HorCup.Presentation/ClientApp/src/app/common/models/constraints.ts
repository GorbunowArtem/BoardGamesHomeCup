export interface ConstraintsViewModel {
  gamesConstraints: GamesConstraints;
  playerConstraints: PlayerConstraints;
}

export interface GamesConstraints {
  titleMaxLength: number;
  minPlayers: number;
  maxPlayers: number;
}

export interface PlayerConstraints {
  maxNameLength: number;
  minBirthDate: string;
}
