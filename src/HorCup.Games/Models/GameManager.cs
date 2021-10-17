using Akkatecture.Aggregates;
using Akkatecture.Commands;

namespace HorCup.Games.Models
{
	public class GameManager: AggregateManager<GameAggregate, GameId, Command<GameAggregate, GameId>>, IGameActorService
	{
		
	}
}