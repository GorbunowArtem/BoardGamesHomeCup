using System;
using System.Collections.Generic;
using HorCup.Presentation.Common.Queries;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Players.Queries.SearchPlayers
{
	public record SearchPlayersQuery: SearchQueryBase, IRequest<(IEnumerable<PlayerViewModel> items, int total)>
	{
		public string SearchText { get; set; } = string.Empty;

		public Guid[] ExceptIds { get; set; } = Array.Empty<Guid>();
	}
}