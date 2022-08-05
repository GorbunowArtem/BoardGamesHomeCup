using System;
using System.Collections.Generic;
using HorCup.Infrastructure.Queries;
using HorCup.Players.ViewModels;
using MediatR;

namespace HorCup.Players.Queries.SearchPlayers;

public record SearchPlayersQuery: SearchQueryBase, IRequest<(IEnumerable<PlayerViewModel> items, int total)>
{
	public string SearchText { get; set; } = string.Empty;

	public Guid[] ExceptIds { get; set; } = Array.Empty<Guid>();
}