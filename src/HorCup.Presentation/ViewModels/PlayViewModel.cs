using System;
using System.Collections.Generic;

namespace HorCup.Presentation.ViewModels
{
	public class PlayViewModel
	{
		public Guid Id { get; set; }

		public IEnumerable<PlayerScoreViewModel> PlayerScores { get; set; }
	}
}