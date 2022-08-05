using System.Linq;
using System.Threading.Tasks;
using HorCup.Plays.Shared.Events;
using HorCup.Statistic.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HorCup.Statistic.PlayersStatistic.Events;

public class PlayerPlayedEventConsumer : IConsumer<GamePlayed>
{
	private readonly IStatisticContext _context;
	private readonly ILogger<PlayerPlayedEventConsumer> _logger;

	public PlayerPlayedEventConsumer(IStatisticContext context, ILogger<PlayerPlayedEventConsumer> logger)
	{
		_context = context;
		_logger = logger;
	}

	public async Task Consume(ConsumeContext<GamePlayed> context)
	{
		foreach (var playerScore in context.Message.PlayedScores)
		{
			var gameId = context.Message.GameId;
			var playerId = playerScore.Player.Id;

			_logger.LogInformation(
				$"Getting statistic for player {playerId} and game {gameId.ToString()}");

			var score = await _context.PlayersStatistics.FirstOrDefaultAsync(
				ps => ps.GameId == gameId && ps.PlayerId == playerId);

			if (score == null)
			{
				_logger.LogInformation(
					$"Player {playerId.ToString()} has no statistic for game {gameId.ToString()}. Adding new...");
				await _context.PlayersStatistics.AddAsync(new PlayerStatistic
				{
					Wins = playerScore.IsWinner ? 1 : 0,
					AverageScore = playerScore.Score,
					GameId = gameId,
					PlayedTotal = 1,
					PlayerId = playerId
				});
			}
			else
			{
				_logger.LogInformation(
					$"Player {playerId.ToString()} has statistic for game {gameId.ToString()}. Updating values...");
				score.Wins = playerScore.IsWinner ? ++score.Wins : score.Wins;
				score.AverageScore = new[] {score.AverageScore, playerScore.Score}.Average();
				score.PlayedTotal += 1;

				_context.PlayersStatistics.Update(score);
			}
		}

		await _context.SaveChangesAsync(default);
	}
}