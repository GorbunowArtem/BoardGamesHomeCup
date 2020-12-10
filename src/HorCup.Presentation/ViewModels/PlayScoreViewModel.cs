namespace HorCup.Presentation.ViewModels
{
	public class PlayScoreViewModel
	{
		public IdName Player { get; set; }
		
		public int? Score { get; set; }

		public bool IsWinner { get; set; }
	}
}