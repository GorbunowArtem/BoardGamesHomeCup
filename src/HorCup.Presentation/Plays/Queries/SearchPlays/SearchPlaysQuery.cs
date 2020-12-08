using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Plays.Queries.SearchPlays
{
	public class SearchPlaysQuery: IRequest<(IEnumerable<PlayViewModel> items, int total)>
	{
		
	}
}