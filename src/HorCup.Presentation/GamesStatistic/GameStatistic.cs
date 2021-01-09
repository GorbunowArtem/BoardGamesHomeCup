using System;
using HorCup.Presentation.Games;

namespace HorCup.Presentation.GamesStatistic
{
	public class GameStatistic
	{
		public Guid Id { get; set; }
		
		public Guid GameId { get; set; }

		public Game Game { get; set; }
		
		public int TimesPlayed { get; set; }

		public double? AverageScore { get; set; }

		public DateTime LastPlayedDate { get; set; }
	}
}