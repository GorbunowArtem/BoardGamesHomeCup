using System;

namespace HorCup.Games.Queries
{
	public class GameProjection
	{
		public Guid Id { get; set; }
		
		public string Title { get; set; }
		
		public int MaxPlayers { get; set; }
		
		public int MinPlayers { get; set; }

	}
}