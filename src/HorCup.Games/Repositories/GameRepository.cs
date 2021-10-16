using System;
using Akkatecture.Aggregates;
using Akkatecture.Subscribers;
using HorCup.Games.Events;
using HorCup.Games.Models;

namespace HorCup.Games.Repositories
{
	public class GameRepository: DomainEventSubscriber,
		ISubscribeTo<GameAggregate, GameId, GameTitleChanged>,
		ISubscribeTo<GameAggregate, GameId, GameMinPlayersChanged>,
		ISubscribeTo<GameAggregate, GameId, GameMaxPlayersChanged>
	{
		public bool Handle(IDomainEvent<GameAggregate, GameId, GameTitleChanged> domainEvent)
		{
			throw new NotImplementedException();
		}

		public bool Handle(IDomainEvent<GameAggregate, GameId, GameMinPlayersChanged> domainEvent)
		{
			throw new NotImplementedException();
		}

		public bool Handle(IDomainEvent<GameAggregate, GameId, GameMaxPlayersChanged> domainEvent)
		{
			throw new NotImplementedException();
		}
	}
}