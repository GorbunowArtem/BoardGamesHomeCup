using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HorCup.Presentation.GamesStatistic.Commands.GamePlayed
{
	public class GamePlayedHandler : INotificationHandler<GamePlayedNotification>
	{
		private readonly IHorCupContext _context;

		public GamePlayedHandler(IHorCupContext context)
		{
			_context = context;
		}

		public async Task Handle(GamePlayedNotification notification, CancellationToken cancellationToken)
		{
			var gameStat =
				await _context.GamesStatistic.FirstOrDefaultAsync(g => g.GameId == notification.GameId,
					cancellationToken);

			if (gameStat == null)
			{
				await _context.GamesStatistic.AddAsync(new GameStatistic
				{
					GameId = notification.GameId,
					AverageScore = notification.AverageScore,
					TimesPlayed = 1,
					LastPlayedDate = notification.LastPlayedDate
				}, cancellationToken);
			}
			else
			{
				gameStat.AverageScore = (gameStat.AverageScore + notification.AverageScore) / 2;
				gameStat.TimesPlayed++;
				gameStat.LastPlayedDate = notification.LastPlayedDate;

				_context.GamesStatistic.Update(gameStat);
			}

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}