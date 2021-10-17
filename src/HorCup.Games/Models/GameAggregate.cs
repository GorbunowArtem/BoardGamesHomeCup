using System;
using HorCup.Games.Events;
using Revo.Domain.Entities.Attributes;
using Revo.Domain.Entities.EventSourcing;

namespace HorCup.Games.Models
{
	[DomainClassId("6872da95-5d19-4b6e-9d9c-221dacab675f")]
	public class GameAggregate : EventSourcedAggregateRoot
	{
		public string Title { get; set; }

		public int MinPlayers { get; set; }

		public int MaxPlayers { get; set; }

		public string Description { get; set; }

		public GameAggregate(Guid id) : base(id)
		{
		}

		public void SetTitle(string title)
		{
			if (!string.IsNullOrWhiteSpace(title) && !string.Equals(title, Title, StringComparison.InvariantCultureIgnoreCase))
			{
				Publish(new GameTitleSet(title));
			}
		}

		public void SetPlayersCount(int minPlayers, int maxPlayers)
		{
			if (minPlayers != MinPlayers
			    || maxPlayers != MaxPlayers)
			{
				Publish(new GamePlayersNumberChanged(minPlayers, maxPlayers));
			}
		}

		public void SetDescription(string description)
		{
			if (!string.Equals(description, Description, StringComparison.InvariantCultureIgnoreCase))
			{
				Publish(new GameDescriptionChanged(description));
			}
		}

		private void Apply(GameTitleSet evt)
		{
			Title = evt.Title;
		}

		private void Apply(GamePlayersNumberChanged evt)
		{
			MaxPlayers = evt.MaxPlayers;
			MinPlayers = evt.MinPlayers;
		}
		
		private void Apply(GameDescriptionChanged evt)
		{
			Description = evt.Description;
		}
	}
}