namespace HorCup.Presentation.Responses
{
	public class PagedSearchResponse<T>
	{
		public int Total { get; set; }

		public T[] Items { get; set; }
	}
}