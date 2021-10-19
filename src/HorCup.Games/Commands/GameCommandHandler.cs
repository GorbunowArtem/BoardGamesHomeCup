using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using HorCup.Games.Models;

namespace HorCup.Games.Commands
{
	public class GameCommandHandler : ICommandHandler<CreateGameCommand>
	{
		private readonly ISession _session;

		public GameCommandHandler(ISession session)
		{
			_session = session;
		}

		public async Task Handle(CreateGameCommand command)
		{
			var game = new GameAggregate(command.Id);

			game.SetTitle(command.Title);
			game.SetPlayersCount(command.MinPlayers, command.MaxPlayers);
			game.SetDescription(command.Description);

			await _session.Add(game);
			await _session.Commit();
		}
	}
}