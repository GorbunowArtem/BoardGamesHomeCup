using System;
using CQRSlite.Events;

namespace HorCup.Games
{
	public class Commit
	{
		public Guid Id { get; set; }

		public IEvent Event { get; set; }

		public Commit(Guid id, IEvent @event)
		{
			Id = id;
			Event = @event;
		}
	}
}