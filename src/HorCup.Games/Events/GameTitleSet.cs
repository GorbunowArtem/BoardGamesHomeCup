using System;
using CQRSlite.Events;

namespace HorCup.Games.Events
{
	public class GameTitleSet : IEvent
	{
		public GameTitleSet(string title)
		{
			Title = title;
		}

		public string Title { get; set; }
		public Guid Id { get; set; }
		public int Version { get; set; }
		public DateTimeOffset TimeStamp { get; set; }
	}
}