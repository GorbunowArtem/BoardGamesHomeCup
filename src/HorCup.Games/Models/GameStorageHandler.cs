using System.Collections.Generic;
using System.Threading.Tasks;
using Akkatecture.Aggregates;
using Akkatecture.Subscribers;
using HorCup.Games.Events;
using HorCup.Games.Queries;

namespace HorCup.Games.Models
{
	public class GameStorageHandler : DomainEventSubscriber,
		ISubscribeToAsync<GameAggregate, GameId, GameTitleChanged>,
		ISubscribeToAsync<GameAggregate, GameId, GameMinPlayersChanged>,
		ISubscribeToAsync<GameAggregate, GameId, GameMaxPlayersChanged>
	{
		private readonly List<GameProjection> _games = new();

		public GameStorageHandler()
		{
			Receive<GetGamesQuery>(Handle);
		}

		public Task HandleAsync(IDomainEvent<GameAggregate, GameId, GameTitleChanged> domainEvent)
		{
			_games.Add(new GameProjection
			{
				Id = domainEvent.AggregateIdentity.GetGuid(),
				Title = domainEvent.AggregateEvent.Title,
			});

			return Task.CompletedTask;
		}

		public bool Handle(GetGamesQuery query)
		{
			Sender.Tell(_games, Self);
			return true;
		}

		public Task HandleAsync(IDomainEvent<GameAggregate, GameId, GameMinPlayersChanged> domainEvent)
		{
			var game = _games.Find(g => g.Id == domainEvent.AggregateIdentity.GetGuid());

			if (game != null)
			{
				game.MinPlayers = domainEvent.AggregateEvent.MinPlayers;
			}

			return Task.CompletedTask;
		}

		public Task HandleAsync(IDomainEvent<GameAggregate, GameId, GameMaxPlayersChanged> domainEvent)
		{
			var game = _games.Find(g => g.Id == domainEvent.AggregateIdentity.GetGuid());

			if (game != null)
			{
				game.MaxPlayers = domainEvent.AggregateEvent.MaxPlayers;
			}

			return Task.CompletedTask;
		}
	}
}