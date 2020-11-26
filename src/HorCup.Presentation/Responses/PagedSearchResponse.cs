using System.Collections.Generic;

namespace HorCup.Presentation.Responses
{
	public class PagedSearchResponse<T>
	{
		public int Total { get; }

		public IEnumerable<T> Items { get; }

		public PagedSearchResponse(IEnumerable<T> items, int total)
		{
			Total = total;
			Items = items;
		}
	}
}