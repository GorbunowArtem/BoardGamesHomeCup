using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.GamesStatistic.Commands.GamePlayed;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Presentation.PlayersStatistic.Commands
{
	public class PlayerPlayedCommandHandler : INotificationHandler<GamePlayedNotification>
	{
		private readonly IHorCupContext _context;
		private readonly ILogger<PlayerPlayedCommandHandler> _logger;

		public PlayerPlayedCommandHandler(IHorCupContext context, ILogger<PlayerPlayedCommandHandler> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task Handle(GamePlayedNotification notification, CancellationToken cancellationToken)
		{
			foreach (var playerScore in notification.PlayerScore)
			{
				_logger.LogInformation(
					$"Getting statistic for player {playerScore.PlayerId.ToString()} and game {notification.GameId.ToString()}");

				var score = await _context.PlayersStatistics.FirstOrDefaultAsync(
					ps => ps.GameId == notification.GameId && ps.PlayerId == playerScore.PlayerId, cancellationToken);

				if (score == null)
				{
					_logger.LogInformation(
						$"Player {playerScore.PlayerId.ToString()} has no statistic for game {notification.GameId.ToString()}. Adding new...");
					await _context.PlayersStatistics.AddAsync(new PlayerStatistic
					{
						Wins = playerScore.IsWinner ? 1 : 0,
						AverageScore = playerScore.Score,
						GameId = notification.GameId,
						PlayedTotal = 1,
						PlayerId = playerScore.PlayerId
					}, cancellationToken);
				}
				else
				{
					_logger.LogInformation(
						$"Player {playerScore.PlayerId.ToString()} has statistic for game {notification.GameId.ToString()}. Updating values...");
					score.Wins = playerScore.IsWinner ? ++score.Wins : score.Wins;
					score.AverageScore = new []{score.AverageScore, playerScore.Score}.Average();
					score.PlayedTotal += 1;

					_context.PlayersStatistics.Update(score);
				}
			}
			
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}