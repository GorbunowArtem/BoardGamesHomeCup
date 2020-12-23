using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Plays.Queries.SearchPlays
{
	public record SearchPlaysQuery: IRequest<(IEnumerable<PlayViewModel> items, int total)>
	{
		public int Take { get; set; } = 10;

		public int Skip { get; set; }
	}
}