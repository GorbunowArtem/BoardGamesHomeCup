using System.Linq;
using System.Threading.Tasks;
using HorCup.Statistic.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Statistic.GamesStatistic.Events.GamePlayed;

public class GamePlayedEventConsumer : IConsumer<Plays.Shared.Events.GamePlayed>
{
	private readonly ILogger<GamePlayedEventConsumer> _logger;
	private readonly IStatisticContext _context;

	public GamePlayedEventConsumer(ILogger<GamePlayedEventConsumer> logger, IStatisticContext context)
	{
		_logger = logger;
		_context = context;
	}

	public async Task Consume(ConsumeContext<Plays.Shared.Events.GamePlayed> context)
	{
		var game = context.Message;

		_logger.LogInformation($"Getting statistic for game {game.GameId.ToString()}");

		var gameStat =
			await _context.GamesStatistics.FirstOrDefaultAsync(g => g.GameId == context.Message.GameId);

		if (gameStat == null || gameStat.TimesPlayed == 0)
		{
			_logger.LogInformation($"Game {game.GameId.ToString()} has no playing history. Adding new...");
			await _context.GamesStatistics.AddAsync(new GameStatistic
			{
				GameId = game.GameId,
				AverageScore = game.AverageScore,
				TimesPlayed = 1,
				LastPlayedDate = game.LastPlayedDate
			});
		}
		else
		{
			_logger.LogInformation(
				$"Game {game.GameId.ToString()} has playing history. Updating values...");
			gameStat.AverageScore = new[] {gameStat.AverageScore, game.AverageScore}.Average();
			gameStat.TimesPlayed++;
			gameStat.LastPlayedDate = game.LastPlayedDate;

			_context.GamesStatistics.Update(gameStat);
		}

		await _context.SaveChangesAsync(default);
	}
}