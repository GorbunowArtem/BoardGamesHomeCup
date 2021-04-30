using System;

namespace HorCup.Games.Models
{
	public class Game
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public int MaxPlayers { get; set; }

		public int MinPlayers { get; set; }

		public DateTime Added { get; set; }

		public bool HasScores { get; set; }
		//
		// public IEnumerable<Play> Plays { get; set; }
		//
		// public GameStatistic GameStatistic { get; set; }
		//
		// public IEnumerable<PlayerStatistic> PlayerStatistics { get; set; }
		//
		// public Game()
		// {
		// 	GameStatistic ??= new GameStatistic();
		// }
	}
}