using HorCup.Presentation.PlayersStatistic;
using HorCup.Tests.Games.Factory;
using HorCup.Tests.Players.Factory;

namespace HorCup.Tests.PlayersStatistic.Queries
{
	public class PlayersStatisticFactory
	{
		public GamesFactory GamesFactory => new();

		public PlayersFactory PlayersFactory => new();

		public PlayerStatistic[] PlayerStatistics => new PlayerStatistic[]
		{
			new()
			{
				GameId = GamesFactory.Game1Id,
				PlayerId = PlayersFactory.Player1Id,
				PlayedTotal = 10,
				Wins = 4,
				AverageScore = 1.1d
			},
			new()
			{
				GameId = GamesFactory.Game2Id,
				PlayerId = PlayersFactory.Player1Id,
				PlayedTotal = 12,
				Wins = 3,
				AverageScore = 12.2d
			},
			new()
			{
				GameId = GamesFactory.Game3Id,
				PlayerId = PlayersFactory.Player1Id,
				PlayedTotal = 13,
				Wins = 2,
				AverageScore = 3.3d
			},
			new()
			{
				GameId = GamesFactory.Game1Id,
				PlayerId = PlayersFactory.Player2Id,
				PlayedTotal = 15,
				Wins = 1,
				AverageScore = 5.5d
			} 
		};
	}
}