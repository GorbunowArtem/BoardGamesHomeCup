using Akkatecture.Aggregates;
using HorCup.Games.Models;

namespace HorCup.Games.Events
{
	public class GameMaxPlayersChanged: AggregateEvent<GameAggregate, GameId>
	{
		public GameMaxPlayersChanged(int maxPlayers)
		{
			MaxPlayers = maxPlayers;
		}

		public int MaxPlayers { get; }
	}
}