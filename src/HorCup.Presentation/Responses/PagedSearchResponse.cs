using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HorCup.Presentation.Responses
{
	[ExcludeFromCodeCoverage]
	public record PagedSearchResponse<T>(IEnumerable<T> Items, int Total);
}