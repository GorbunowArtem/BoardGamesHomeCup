using System;
using System.Collections.Generic;

namespace HorCup.Games.Queries.SearchGames
{
	public record SearchGamesQuery 
	{
		public string SearchText { get; set; }

		public int? MinPlayers { get; set; }

		public int? MaxPlayers { get; set; }

		public IEnumerable<Guid> ExceptIds { get; set; }
	}
}