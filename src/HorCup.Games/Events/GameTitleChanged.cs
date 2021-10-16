using Akkatecture.Aggregates;
using HorCup.Games.Models;

namespace HorCup.Games.Events
{
	public class GameTitleChanged: AggregateEvent<GameAggregate, GameId>
	{
		public GameTitleChanged(string title)
		{
			Title = title;
		}

		public string Title { get; }
	}
}