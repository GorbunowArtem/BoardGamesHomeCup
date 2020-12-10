using System;
using HorCup.Presentation.Players;
using HorCup.Presentation.Plays;

namespace HorCup.Presentation.PlayScores
{
	public class PlayScore
	{
		public Guid Id { get; set; }
		
		public Guid PlayId { get; set; }

		public Play Play { get; set; }
		
		public Guid PlayerId { get; set; }

		public Player Player { get; set; }
		
		public int? Score { get; set; }

		public bool IsWinner { get; set; }
	}
}