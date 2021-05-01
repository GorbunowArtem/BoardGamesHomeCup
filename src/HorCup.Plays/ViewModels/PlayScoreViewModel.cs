namespace HorCup.Presentation.ViewModels
{
	public record PlayScoreViewModel(
		IdName Player,
		int? Score,
		bool IsWinner);
}