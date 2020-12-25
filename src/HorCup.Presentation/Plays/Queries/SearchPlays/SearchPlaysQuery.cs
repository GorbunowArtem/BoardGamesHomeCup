using System;
using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Plays.Queries.SearchPlays
{
	public record SearchPlaysQuery: IRequest<(IEnumerable<PlayViewModel> items, int total)>
	{
		public int Take { get; set; } = 10;

		public int Skip { get; set; }

		public IEnumerable<Guid> GamesIds { get; set; }
		
		public IEnumerable<Guid> PlayersIds { get; set; }

		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

		public SearchPlaysQuery()
		{
			GamesIds = Array.Empty<Guid>();
			PlayersIds = Array.Empty<Guid>();
		}
	}
}