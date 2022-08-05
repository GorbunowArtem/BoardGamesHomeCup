using HorCup.Infrastructure.ViewModels;

namespace HorCup.Plays.Models;

public class PlayScore
{
	public IdName Player { get; set; }

	public int Score { get; set; }

	public bool IsWinner { get; set; }
}