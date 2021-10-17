using Revo.Domain.Events;

namespace HorCup.Games.Events
{
	public class GameDescriptionChanged: DomainAggregateEvent
	{
		public GameDescriptionChanged(string description)
		{
			Description = description;
		}

		public string Description { get; }
	}
}