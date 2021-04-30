using System;
using System.Collections.Generic;
using HorCup.Infrastructure.Queries;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Games.Queries.SearchGames
{
	public record SearchGamesQuery : SearchQueryBase, IRequest<(IEnumerable<GameViewModel> items, int total)>
	{
		public string SearchText { get; set; }

		public int? MinPlayers { get; set; }

		public int? MaxPlayers { get; set; }

		public IEnumerable<Guid> ExceptIds { get; set; }
	}
}