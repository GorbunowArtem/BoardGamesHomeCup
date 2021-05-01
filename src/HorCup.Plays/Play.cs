using System;
using System.Collections.Generic;
using HorCup.Presentation.PlayScores;

namespace HorCup.Presentation.Plays
{
	public class Play
	{
		public Guid Id { get; set; }

		// public Game Game { get; set; }
		
		public Guid GameId { get; set; }

		public DateTime PlayedDate { get; set; }

		public string Notes { get; set; }

		public IEnumerable<PlayScore> PlayerScores { get; set; }
	}
}