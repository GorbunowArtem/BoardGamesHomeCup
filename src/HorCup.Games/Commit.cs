using CQRSlite.Events;
using MongoDB.Bson;

namespace HorCup.Games
{
	public class Commit
	{
		public ObjectId Id { get; set; }

		public IEvent Event { get; set; }

		public Commit(IEvent @event)
		{
			Event = @event;
		}
	}
}