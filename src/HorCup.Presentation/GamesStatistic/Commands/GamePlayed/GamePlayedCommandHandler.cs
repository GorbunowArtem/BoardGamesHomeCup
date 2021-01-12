using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.GamesStatistic.Commands.GamePlayed
{
	public class GamePlayedCommandHandler : INotificationHandler<GamePlayedNotification>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<GamePlayedCommandHandler> _logger;

		public GamePlayedCommandHandler(IHorCupContext context, ILogger<GamePlayedCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task Handle(GamePlayedNotification notification, CancellationToken cancellationToken)
		{
			_logger.LogInformation($"Getting statistic for game {notification.GameId.ToString()}");

			var gameStat =
				await _context.GamesStatistics.FirstOrDefaultAsync(g => g.GameId == notification.GameId,
					cancellationToken);

			if (gameStat == null || gameStat.TimesPlayed == 0)
			{
				_logger.LogInformation($"Game {notification.GameId.ToString()} has no playing history. Adding new...");
				await _context.GamesStatistics.AddAsync(new GameStatistic
				{
					GameId = notification.GameId,
					AverageScore = notification.AverageScore,
					TimesPlayed = 1,
					LastPlayedDate = notification.LastPlayedDate
				}, cancellationToken);
			}
			else
			{
				_logger.LogInformation(
					$"Game {notification.GameId.ToString()} has playing history. Updating values...");
				gameStat.AverageScore = (gameStat.AverageScore + notification.AverageScore) / 2;
				gameStat.TimesPlayed++;
				gameStat.LastPlayedDate = notification.LastPlayedDate;

				_context.GamesStatistics.Update(gameStat);
			}

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}