using System;
using System.Collections.Generic;

namespace HorCup.Presentation.ViewModels
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