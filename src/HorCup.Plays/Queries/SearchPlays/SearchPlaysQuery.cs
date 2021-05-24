using System;
using System.Collections.Generic;
using HorCup.Infrastructure.Queries;
using HorCup.Plays.ViewModels;
using MediatR;

namespace HorCup.Plays.Queries.SearchPlays
{
	public record SearchPlaysQuery : SearchQueryBase, IRequest<(IEnumerable<PlayViewModel> items, long total)>
	{
		public IEnumerable<Guid> GamesIds { get; set; } = Array.Empty<Guid>();

		public IEnumerable<Guid> PlayersIds { get; set; } = Array.Empty<Guid>();

		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

		public string SearchText { get; set; }
	}
}