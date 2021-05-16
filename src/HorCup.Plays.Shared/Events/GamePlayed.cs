using System;
using HorCup.Infrastructure.Events;

namespace HorCup.Plays.Shared.Events
{
	public class GamePlayed: IntegrationEvent
	{
		public Guid GameId { get; set; }
	}
}