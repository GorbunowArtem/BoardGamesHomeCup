using System.Diagnostics;
using System.Threading.Tasks;
using Akkatecture.Aggregates;
using Akkatecture.Subscribers;
using HorCup.Games.Events;
using Microsoft.Extensions.Logging;

namespace HorCup.Games.Models
{
	public class GameSubscriber : DomainEventSubscriber,
		ISubscribeToAsync<GameAggregate, GameId, GameTitleChanged>
	{
		private readonly ILogger<GameSubscriber> _logger;

		public GameSubscriber(ILogger<GameSubscriber> logger)
		{
			_logger = logger;
		}

		public Task HandleAsync(IDomainEvent<GameAggregate, GameId, GameTitleChanged> domainEvent)
		{
			throw new System.NotImplementedException();
		}
	}
}