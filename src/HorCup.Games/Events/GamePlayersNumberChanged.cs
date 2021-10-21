using System;
using CQRSlite.Events;

namespace HorCup.Games.Events
{
	public class GamePlayersNumberChanged : IEvent
	{
		public GamePlayersNumberChanged(int minPlayers, int maxPlayers)
		{
			MinPlayers = minPlayers;
			MaxPlayers = maxPlayers;
		}

		public int MinPlayers { get; set; }

		public int MaxPlayers { get; set; }
		public Guid Id { get; set; }
		public int Version { get; set; }
		public DateTimeOffset TimeStamp { get; set; }
	}
}