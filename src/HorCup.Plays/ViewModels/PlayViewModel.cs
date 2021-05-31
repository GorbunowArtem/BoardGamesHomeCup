using System;
using System.Collections.Generic;

namespace HorCup.Plays.ViewModels
{
	public class PlayViewModel
	{
		public Guid Id { get; set; }

		public IEnumerable<PlayScoreViewModel> PlayerScores { get; set; }
		
		public Guid GameId { get; set; }

		public string GameTitle { get; set; }

		public DateTime PlayedDate { get; set; }

		public string Notes { get; set; }
	}
}