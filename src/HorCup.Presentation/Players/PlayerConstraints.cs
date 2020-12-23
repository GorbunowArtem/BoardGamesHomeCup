using System;

namespace HorCup.Presentation.Players
{
	public class PlayerConstraints
	{
		public int MaxNameLength => 15;
		
		public DateTime MinBirthDate => new DateTime(1900, 1, 1);
	}
}