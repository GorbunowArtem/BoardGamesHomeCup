using System;
using HorCup.Players.Shared.ViewModels;
using HorCup.Presentation.Games;
using HorCup.Presentation.ViewModels;

namespace HorCup.Presentation.PlayersStatistic
{
	public class PlayerStatistic
	{
		public Guid PlayerId { get; set; }

		public PlayerViewModel Player { get; set; }
		
		public Guid GameId { get; set; }

		public Game Game { get; set; }
		
		public int PlayedTotal { get; set; }

		public int Wins { get; set; }

		public double? AverageScore { get; set; }
	}
}