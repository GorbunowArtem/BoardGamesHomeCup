using System.Threading;
using System.Threading.Tasks;
using HorCup.Presentation.Context;
using HorCup.Presentation.GamesStatistic.Commands.GamePlayed;
using MediatR;

namespace HorCup.Presentation.PlayersStatistic.Commands
{
	public class PlayerPlayedCommandHandler : INotificationHandler<GamePlayedNotification>
	{
		private readonly IHorCupContext _context;

		public PlayerPlayedCommandHandler(IHorCupContext context)
		{
			_context = context;
		}

		public async Task Handle(GamePlayedNotification notification, CancellationToken cancellationToken)
		{
			
		}
	}
}