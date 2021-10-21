using System;
using CQRSlite.Events;

namespace HorCup.Games.Events
{
	public class GameDescriptionChanged: IEvent
	{
		public GameDescriptionChanged(string description)
		{
			Description = description;
		}

		public string Description { get; set; }
		public Guid Id { get; set; }
		public int Version { get; set; }
		public DateTimeOffset TimeStamp { get; set; }
	}
}