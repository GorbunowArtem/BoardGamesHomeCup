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
				var score = await _context.PlayersStatistics.FirstOrDefaultAsync(
					ps => ps.GameId == notification.GameId && ps.PlayerId == playerScore.PlayerId, cancellationToken);

				if (score == null)
				{
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
					score.Wins = playerScore.IsWinner ? ++score.Wins : score.Wins;
					score.AverageScore = (score.AverageScore + playerScore.Score) / 2;
					score.PlayedTotal += 1;

					_context.PlayersStatistics.Update(score);
				}
			}
			
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}