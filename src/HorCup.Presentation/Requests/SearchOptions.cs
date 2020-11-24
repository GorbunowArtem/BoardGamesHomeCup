namespace HorCup.Presentation.Requests
{
	public class SearchOptions
	{
		public int Take { get; } = 10;

		public int Skip { get; set; }

		public string SearchText { get; set; } = string.Empty;
	}
}