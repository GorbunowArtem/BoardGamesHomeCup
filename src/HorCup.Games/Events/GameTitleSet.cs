using Revo.Domain.Events;

namespace HorCup.Games.Events
{
	public class GameTitleSet : DomainAggregateEvent
	{
		public GameTitleSet(string title)
		{
			Title = title;
		}

		public string Title { get; }

		
	}
}