using System;
using System.Diagnostics.CodeAnalysis;

namespace HorCup.Presentation.Players
{
	[ExcludeFromCodeCoverage]
	public class PlayerConstraints
	{
		public int MaxNameLength => 20;
		
		public DateTime MinBirthDate => new(1900, 1, 1);
	}
}