using System;
using HorCup.Presentation.Players;

namespace HorCup.Presentation.ViewModels
{
	public class PlayerStatisticViewModel
	{
		public Guid PlayerId { get; set; }

		public Player Player { get; set; }
		
		public Guid GameId { get; set; }

		public int PlayedTotal { get; set; }

		public int Wins { get; set; }

		public double? AverageScore { get; set; }

	}
}