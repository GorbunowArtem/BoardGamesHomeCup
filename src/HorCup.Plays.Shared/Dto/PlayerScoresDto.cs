using HorCup.Infrastructure.ViewModels;

namespace HorCup.Plays.Shared.Dto;

public class PlayerScoresDto
{
	public IdName Player { get; set; }

	public int Score { get; set; }

	public bool IsWinner { get; set; }
}