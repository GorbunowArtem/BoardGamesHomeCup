using System;
using System.Collections.Generic;
using HorCup.Presentation.PlayScores;

namespace HorCup.Presentation.Players
{
	public class Player
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Nickname { get; set; }
		
		public DateTime BirthDate { get; set; }

		public DateTime Added { get; set; }

		public IEnumerable<PlayScore> PlayScores { get; set; }
	}
}