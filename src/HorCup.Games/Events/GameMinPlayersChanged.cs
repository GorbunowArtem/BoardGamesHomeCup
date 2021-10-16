using Akkatecture.Aggregates;
using HorCup.Games.Models;

namespace HorCup.Games.Events
{
	public class GameMinPlayersChanged : AggregateEvent<GameAggregate, GameId>
	{
		public GameMinPlayersChanged(int minPlayers)
		{
			MinPlayers = minPlayers;
		}

		public int MinPlayers { get; }
	}
}