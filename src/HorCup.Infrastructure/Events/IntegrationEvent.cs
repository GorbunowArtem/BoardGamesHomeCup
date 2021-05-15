using System;

namespace HorCup.Infrastructure.Events
{
	public record IntegrationEvent
	{
		public IntegrationEvent()
		{
			Id = Guid.NewGuid();
			Created = DateTime.UtcNow;
		}

		public Guid Id { get; init; }

		public DateTime Created { get; set; }
	}
}