using System;

namespace HorCup.Infrastructure.Events
{
	public class IntegrationEvent
	{
		public Guid EventId { get; set; } = Guid.NewGuid();

		public DateTime Created { get; set; } = DateTime.UtcNow;
	}
}