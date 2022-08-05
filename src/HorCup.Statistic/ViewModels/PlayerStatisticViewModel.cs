using System;

namespace HorCup.Statistic.ViewModels;

public class PlayerStatisticViewModel
{
	public Guid PlayerId { get; set; }

	// public PlayerViewModel Player { get; set; }
		
	public Guid GameId { get; set; }

	// public GameViewModel Game { get; set; }
		
	public int PlayedTotal { get; set; }

	public int Wins { get; set; }

	public double? AverageScore { get; set; }
}