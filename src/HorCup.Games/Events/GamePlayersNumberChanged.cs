using Revo.Domain.Events;

namespace HorCup.Games.Events
{
	public class GamePlayersNumberChanged : DomainAggregateEvent
	{
		public GamePlayersNumberChanged(int minPlayers, int maxPlayers)
		{
			MinPlayers = minPlayers;
			MaxPlayers = maxPlayers;
		}

		public int MinPlayers { get; }

		public int MaxPlayers { get; }
	}
}