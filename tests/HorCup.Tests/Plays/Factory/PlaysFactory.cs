using System;
using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using HorCup.Tests.Games.Factory;

namespace HorCup.Tests.Plays.Factory
{
	public class PlaysFactory
	{
		public readonly Guid Play1Id = 12.Guid();
		
		public const string Notes1 = "Play 1 notes";
		
		public GamesFactory Games => new();
		
		public Commands Commands => new(this);

		public static IEnumerable<PlayScoreViewModel> Play1Scores => new PlayScoreViewModel[]
		{
			new(new IdName(1.Guid(), "Player1"), 11, false),
			new(new IdName(2.Guid(), "Player2"), 22, false),
			new(new IdName(3.Guid(), "Player3"), 33, true),
		};

		public static IEnumerable<PlayScoreViewModel> Play2Scores => new PlayScoreViewModel[]
		{
			new(new IdName(1.Guid(), "Player1"), 11, false),
			new(new IdName(2.Guid(), "Player2"), 22, false),
			new(new IdName(3.Guid(), "Player3"), 32, false),
			new(new IdName(4.Guid(), "Player4"), 42, false),
			new(new IdName(5.Guid(), "Player5"), 52, false),
			new(new IdName(6.Guid(), "Player6"), 63, false),
			new(new IdName(7.Guid(), "Player7"), 73, true),
		};
	}
}