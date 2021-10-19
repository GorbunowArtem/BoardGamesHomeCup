using System;
using CQRSlite.Domain;
using HorCup.Games.Events;

namespace HorCup.Games.Models
{
	public class GameAggregate : AggregateRoot
	{
		public string Title { get; set; }

		public int MinPlayers { get; set; }

		public int MaxPlayers { get; set; }

		public string Description { get; set; }

		public GameAggregate(Guid id)
		{
			Id = id;
		}
		
		public void SetTitle(string title)
		{
			if (!string.IsNullOrWhiteSpace(title) && !string.Equals(title, Title, StringComparison.InvariantCultureIgnoreCase))
			{
				ApplyChange(new GameTitleSet(title));
			}
		}

		public void SetPlayersCount(int minPlayers, int maxPlayers)
		{
			if (minPlayers != MinPlayers
			    || maxPlayers != MaxPlayers)
			{
				ApplyChange(new GamePlayersNumberChanged(minPlayers, maxPlayers));
			}
		}

		public void SetDescription(string description)
		{
			if (!string.Equals(description, Description, StringComparison.InvariantCultureIgnoreCase))
			{
				ApplyChange(new GameDescriptionChanged(description));
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