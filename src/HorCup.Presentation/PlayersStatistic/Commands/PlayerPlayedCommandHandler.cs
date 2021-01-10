using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.GamesStatistic.Commands.GamePlayed;
using MediatR;
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
			
		}
	}
}