using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HorCup.Statistic.GamesStatistic.Events.GamePlayed
{
	public class GamePlayedEventConsumer : IConsumer<Plays.Shared.Events.GamePlayed>
	{
		private readonly ILogger<GamePlayedEventConsumer> _logger;

		public GamePlayedEventConsumer(ILogger<GamePlayedEventConsumer> logger)
		{
			_logger = logger;
		}

		// public async Task Handle(Plays.Shared.Events.GamePlayed notification, CancellationToken cancellationToken)
		// {
		// 	_logger.LogInformation($"Getting statistic for game {notification.GameId.ToString()}");
		//
		// 	// var gameStat =
		// 	// 	await _context.GamesStatistics.FirstOrDefaultAsync(g => g.GameId == notification.GameId,
		// 	// 		cancellationToken);
		// 	//
		// 	// if (gameStat == null || gameStat.TimesPlayed == 0)
		// 	// {
		// 	// 	_logger.LogInformation($"Game {notification.GameId.ToString()} has no playing history. Adding new...");
		// 	// 	await _context.GamesStatistics.AddAsync(new GameStatistic
		// 	// 	{
		// 	// 		GameId = notification.GameId,
		// 	// 		AverageScore = notification.AverageScore,
		// 	// 		TimesPlayed = 1,
		// 	// 		LastPlayedDate = notification.LastPlayedDate
		// 	// 	}, cancellationToken);
		// 	// }
		// 	// else
		// 	// {
		// 	// 	_logger.LogInformation(
		// 	// 		$"Game {notification.GameId.ToString()} has playing history. Updating values...");
		// 	// 	gameStat.AverageScore = new [] {gameStat.AverageScore, notification.AverageScore}.Average();
		// 	// 	gameStat.TimesPlayed++;
		// 	// 	gameStat.LastPlayedDate = notification.LastPlayedDate;
		// 	//
		// 	// 	_context.GamesStatistics.Update(gameStat);
		// 	// }
		// 	//
		// 	// await _context.SaveChangesAsync(cancellationToken);
		// }

		public Task Consume(ConsumeContext<Plays.Shared.Events.GamePlayed> context)
		{
			Console.WriteLine($"Added game stats {context.Message.GameId}");
			
			_logger.LogDebug($"Added game stats {context.Message.GameId}");

			return Task.CompletedTask;
		}
	}
}