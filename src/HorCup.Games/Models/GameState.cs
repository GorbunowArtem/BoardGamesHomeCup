using System.Diagnostics;
using Akkatecture.Aggregates;
using HorCup.Games.Events;

namespace HorCup.Games.Models
{
	public class GameState : AggregateState<GameAggregate, GameId>,
		IApply<GameTitleChanged>,
		IApply<GameMaxPlayersChanged>,
		IApply<GameMinPlayersChanged>
	{
		public string Title { get; set; }
		
		public int MaxPlayers { get; set; }
		
		public int MinPlayers { get; set; }

		public void Apply(GameTitleChanged aggregateEvent)
		{
			Title = aggregateEvent.Title;
		}

		public void Apply(GameMaxPlayersChanged aggregateEvent)
		{
			MaxPlayers = aggregateEvent.MaxPlayers;
		}

		public void Apply(GameMinPlayersChanged aggregateEvent)
		{
			MinPlayers = aggregateEvent.MinPlayers;
		}
	}
}