using HorCup.Presentation.GamesStatistic.Commands.GamePlayed;

namespace HorCup.Tests.GamesStatistic.Factory
{
	public class Commands
	{
		private readonly GamesStatisticsFactory _factory;

		public Commands(GamesStatisticsFactory factory)
		{
			_factory = factory;
		}

		public GamePlayedNotification GamePlayedNotification => new GamePlayedNotification(
			_factory.CreatedGameStatisticId,
			GamesStatisticsFactory.CreatedGameAvgScore,
			_factory.CreatedGameLastPlayedDate,
			_factory.CreatedGamePlayerScores);
	}
}