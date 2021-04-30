using System;
using HorCup.Players.Shared.ViewModels;
using HorCup.Presentation.ViewModels;

namespace HorCup.Presentation.PlayScores
{
	public class PlayScore
	{
		public Guid Id { get; set; }
		
		public Guid PlayId { get; set; }

		public Guid PlayerId { get; set; }

		public PlayerViewModel Player { get; set; }
		
		public int? Score { get; set; }

		public bool IsWinner { get; set; }
	}
}