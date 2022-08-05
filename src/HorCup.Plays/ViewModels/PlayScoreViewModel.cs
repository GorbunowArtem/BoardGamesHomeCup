using HorCup.Infrastructure.ViewModels;

namespace HorCup.Plays.ViewModels;

public record PlayScoreViewModel(
	IdName Player,
	int? Score,
	bool IsWinner);