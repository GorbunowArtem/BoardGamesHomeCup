using System;
using System.Collections.Generic;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.ViewModels;
using HorCup.Tests.Games.Factory;

namespace HorCup.Tests.Plays.Factory
{
	public class PlaysFactory
	{
		public readonly Guid CreatedPlay1Id = 12.Guid();

		public readonly Guid Play1Id = 13.Guid();
		public readonly Guid Play2Id = 14.Guid();
		public readonly Guid Play3Id = 15.Guid();
		public readonly Guid Play4Id = 16.Guid();
		public readonly Guid Play5Id = 17.Guid();

		public readonly DateTime Play1Date = TestExtensions.ToDateTime(2020, 1, 1);
		public readonly DateTime Play2Date = TestExtensions.ToDateTime(2020, 2, 2);
		public readonly DateTime Play3Date = TestExtensions.ToDateTime(2020, 3, 3);
		public readonly DateTime Play4Date = TestExtensions.ToDateTime(2020, 4, 4);
		public readonly DateTime Play5Date = TestExtensions.ToDateTime(2020, 5, 5);

		public const string Notes1 = "Play 1 notes";

		public GamesFactory Games => new();

		public Commands Commands => new(this);

		public IEnumerable<Play> Plays => new Play[]
		{
			new()
			{
				Id = Play1Id,
				GameId = Games.Game1Id,
				PlayedDate = Play1Date,
			},
			new()
			{
				Id = Play2Id,
				GameId = Games.Game2Id,
				PlayedDate = Play2Date,
			},
			new()
			{
				Id = Play3Id,
				PlayedDate = Play3Date,
				GameId = Games.Game3Id,
			},
			new()
			{
				Id = Play4Id,
				PlayedDate = Play4Date,
				GameId = Games.Game4Id,
			},
			new()
			{
				Id = Play5Id,
				PlayedDate = Play5Date,
				GameId = Games.Game4Id,
			}
		};

		public IEnumerable<PlayScore> PlayScores => new PlayScore[]
		{
			new()
			{
				Id = 11.Guid(),
				Score = 11,
				IsWinner = false,
				PlayerId = 11.Guid(),
				PlayId = Play1Id
			},
			new()
			{
				Id = 22.Guid(),
				Score = 22,
				IsWinner = false,
				PlayerId = 22.Guid(),
				PlayId = Play1Id
			},
			new()
			{
				Id = 33.Guid(),
				Score = 33,
				IsWinner = true,
				PlayerId = 33.Guid(),
				PlayId = Play1Id
			},

			new()
			{
				Id = 44.Guid(),
				Score = 111,
				IsWinner = false,
				PlayerId = 111.Guid(),
				PlayId = Play2Id
			},
			new()
			{
				Id = 55.Guid(),
				Score = 222,
				IsWinner = true,
				PlayerId = 222.Guid(),
				PlayId = Play2Id
			},
			new()
			{
				Id = 66.Guid(),
				Score = 111,
				IsWinner = false,
				PlayerId = 111.Guid(),
				PlayId = Play3Id
			},
			new()
			{
				Id = 77.Guid(),
				Score = 222,
				IsWinner = true,
				PlayerId = 222.Guid(),
				PlayId = Play3Id
			},
			new()
			{
				Id = 88.Guid(),
				Score = 22,
				IsWinner = false,
				PlayerId = 22.Guid(),
				PlayId = Play4Id
			},
			new()
			{
				Id = 99.Guid(),
				Score = 33,
				IsWinner = true,
				PlayerId = 33.Guid(),
				PlayId = Play4Id
			},
			new()
			{
				Id = 111.Guid(),
				Score = 11,
				IsWinner = false,
				PlayerId = 11.Guid(),
				PlayId = Play4Id
			},
			new()
			{
				Id = 222.Guid(),
				Score = 222,
				IsWinner = false,
				PlayerId = 44444.Guid(),
				PlayId = Play5Id
			},
			new()
			{
				Id = 333.Guid(),
				Score = 555,
				IsWinner = true,
				PlayerId = 55555.Guid(),
				PlayId = Play5Id
			},
		};

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