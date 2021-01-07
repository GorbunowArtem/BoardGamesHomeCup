using System;
using System.Collections.Generic;
using HorCup.Presentation.GamesStatistic;
using HorCup.Presentation.Plays;

namespace HorCup.Presentation.Games
{
	public class Game
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public int MaxPlayers { get; set; }

		public int MinPlayers { get; set; }

		public DateTime Added { get; set; }

		public bool HasScores { get; set; }

		public IEnumerable<Play> Plays { get; set; }

		public GameStatistic GameStatistic { get; set; }
	}
}