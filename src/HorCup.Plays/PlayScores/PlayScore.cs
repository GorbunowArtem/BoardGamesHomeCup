using System;

namespace HorCup.Plays.PlayScores
{
	public class PlayScore
	{
		public Guid Id { get; set; }
		
		public Guid PlayId { get; set; }

		public Guid PlayerId { get; set; }

		// public PlayerViewModel Player { get; set; }
		
		public int? Score { get; set; }

		public bool IsWinner { get; set; }
	}
}