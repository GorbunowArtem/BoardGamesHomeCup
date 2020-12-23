using System.Collections.Generic;

namespace HorCup.Presentation.Responses
{
	public record PagedSearchResponse<T>(IEnumerable<T> Items, int Total);
}