using System;

namespace HorCup.Presentation.ViewModels
{
	public class PlayerScoreViewModel
	{
		public Guid PlayerId { get; set; }

		public int? Score { get; set; }

		public bool IsWinner { get; set; }
	}
}