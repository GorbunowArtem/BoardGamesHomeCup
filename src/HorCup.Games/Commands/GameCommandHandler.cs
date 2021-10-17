using System.Threading;
using System.Threading.Tasks;
using HorCup.Games.Models;
using Revo.Core.Commands;
using Revo.Infrastructure.Repositories;

namespace HorCup.Games.Commands
{
	public class GameCommandHandler: ICommandHandler<CreateGameCommand>
	{
		private readonly IRepository _repository;

		public GameCommandHandler(IRepository repository)
		{
			_repository = repository;
		}

		public Task HandleAsync(CreateGameCommand command, CancellationToken cancellationToken)
		{
			var game = new GameAggregate(command.Id);
			
			game.SetTitle(command.Title);
			game.SetPlayersCount(command.MinPlayers, command.MaxPlayers);
			game.SetDescription(command.Description);
			
			_repository.Add(game);
			
			return Task.CompletedTask;
		}
	}
}