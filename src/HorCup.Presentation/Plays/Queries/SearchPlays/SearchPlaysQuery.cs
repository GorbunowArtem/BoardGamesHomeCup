using System;
using System.Collections.Generic;
using HorCup.Presentation.Common.Queries;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Plays.Queries.SearchPlays
{
	public record SearchPlaysQuery: SearchQueryBase, IRequest<(IEnumerable<PlayViewModel> items, int total)>
	{
		public IEnumerable<Guid> GamesIds { get; set; }
		
		public IEnumerable<Guid> PlayersIds { get; set; }

		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }
	}
}