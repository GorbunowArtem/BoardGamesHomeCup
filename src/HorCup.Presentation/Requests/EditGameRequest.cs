namespace HorCup.Presentation.Requests
{
	public record EditGameRequest(
		string Title,
		int MaxPlayers,
		int MinPlayers,
		bool HasScores);
}