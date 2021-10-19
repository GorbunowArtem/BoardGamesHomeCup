using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using CQRSlite.Queries;
using HorCup.Games.Events;
using HorCup.Games.Models;
using HorCup.Games.Queries;
using HorCup.Infrastructure.Exceptions;

namespace HorCup.Games.Projections
{
	public class GamesProjection : ICancellableEventHandler<GameTitleSet>,
		ICancellableEventHandler<GamePlayersNumberChanged>,
		ICancellableEventHandler<GameDescriptionChanged>,
		ICancellableQueryHandler<GetGameByIdQuery, GameDto>
	{
		public Task Handle(GameTitleSet message, CancellationToken token = new CancellationToken())
		{
			InMemoryDatabase.Games.Add(message.Id, new GameDto
			{
				Id = message.Id,
				Title = message.Title
			});

			return Task.CompletedTask;
		}

		public Task<GameDto> Handle(GetGameByIdQuery message, CancellationToken token = new CancellationToken())
		{
			if (InMemoryDatabase.Games.TryGetValue(message.Id, out var game))
			{
				return Task.FromResult(game);
			}

			throw new NotFoundException();
		}

		public Task Handle(GamePlayersNumberChanged message, CancellationToken token = new CancellationToken())
		{
			var game = InMemoryDatabase.Games[message.Id];

			game.MaxPlayers = message.MaxPlayers;
			game.MinPlayers = message.MinPlayers;

			return Task.CompletedTask;
		}

		public Task Handle(GameDescriptionChanged message, CancellationToken token = new CancellationToken())
		{
			var game = InMemoryDatabase.Games[message.Id];

			game.Description = message.Description;

			return Task.CompletedTask;
		}
	}
}