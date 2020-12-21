import { ConstraintsViewModel } from '../models/constraints';

export const constraints1: ConstraintsViewModel = {
  gamesConstraints: {
    maxPlayers: 1,
    minPlayers: 2,
    titleMaxLength: 23
  },
  playerConstraints: {
    maxNameLength: 1,
    minBirthDate: ''
  }
};
