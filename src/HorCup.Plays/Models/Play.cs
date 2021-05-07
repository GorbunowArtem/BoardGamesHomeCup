using System;
using System.Collections.Generic;

namespace HorCup.Plays.Models
{
	public class Play
	{
		public Guid Id { get; set; }

		public Game Game { get; set; }

		public DateTime PlayedDate { get; set; }

		public string Notes { get; set; }

		public IEnumerable<PlayScore> PlayerScores { get; set; }
	}
}