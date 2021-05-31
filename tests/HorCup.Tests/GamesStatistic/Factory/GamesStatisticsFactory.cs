// using System;
// using System.Collections.Generic;
// using HorCup.Presentation.GamesStatistic;
// using HorCup.Presentation.PlayScores;
// using HorCup.Tests.Games.Factory;
//
// namespace HorCup.Tests.GamesStatistic.Factory
// {
// 	public class GamesStatisticsFactory
// 	{
// 		public readonly GamesFactory GamesFactory = new();
// 		// public readonly PlayersFactory PlayersFactory = new();
//
// 		public readonly Guid CreatedGameStatisticId = 4434.Guid();
// 		public readonly Guid Game1StatId = 123.Guid();
// 		public readonly Guid Game2StatId = 124.Guid();
// 		public readonly Guid Game3StatId = 125.Guid();
//
// 		public const double CreatedGameAvgScore = 9.3d;
// 		public const double Game1AvgScore = 11.3d;
// 		public const double Game2AvgScore = 22.5d;
// 		public const double Game3AvgScore = 44.53d;
//
// 		public const int CreatedGameTimesPlayed = 4;
// 		public const int Game1TimesPlayed = 2;
// 		public const int Game2TimesPlayed = 4;
// 		public const int Game3TimesPlayed = 16;
//
// 		
// 		public const int Player1Game1Score = 11;
// 		public const int Player2Game1Score = 22;
// 		
// 		public const int Player1Game2Score = 33;
// 		public const int Player2Game2Score = 56;
//
// 		public readonly DateTime CreatedGameLastPlayedDate = TestExtensions.ToDateTime(2020, 3, 3);
// 		public readonly DateTime Game1LastPlayedDate = TestExtensions.ToDateTime(2020, 5, 5);
// 		public readonly DateTime Game2LastPlayedDate = TestExtensions.ToDateTime(2020, 6, 6);
// 		public readonly DateTime Game3LastPlayedDate = TestExtensions.ToDateTime(2020, 7, 7);
//
// 		public Commands Commands => new(this);
//
// 		public GameStatistic[] GameStatistics =>
// 			new GameStatistic[]
// 			{
// 				new()
// 				{
// 					Id = Game1StatId,
// 					GameId = GamesFactory.Game1Id,
// 					AverageScore = Game1AvgScore,
// 					TimesPlayed = Game1TimesPlayed,
// 					LastPlayedDate = Game1LastPlayedDate
// 				},
// 				new()
// 				{
// 					Id = Game2StatId,
// 					GameId = GamesFactory.Game2Id,
// 					AverageScore = Game2AvgScore,
// 					TimesPlayed = Game2TimesPlayed,
// 					LastPlayedDate = Game2LastPlayedDate
// 				},
// 				new()
// 				{
// 					Id = Game3StatId,
// 					GameId = GamesFactory.Game3Id,
// 					AverageScore = Game3AvgScore,
// 					TimesPlayed = Game3TimesPlayed,
// 					LastPlayedDate = Game3LastPlayedDate
// 				}
// 			};
//
// 		public IEnumerable<PlayScore> CreatedGamePlayerScores => new PlayScore[]
// 		{
// 			new ()
// 			{
// 				Score = Player1Game1Score,
// 				IsWinner = false,
// 				// PlayerId = PlayersFactory.Player1Id
// 			},
// 			new ()
// 			{
// 				Score = Player2Game1Score,
// 				IsWinner = true,
// 				// PlayerId = PlayersFactory.Player2Id
// 			}
// 		};
//
// 		public IEnumerable<PlayScore> UpdatedGamePlayerScores1 => new PlayScore[]
// 		{
// 			new ()
// 			{
// 				Score = Player1Game2Score,
// 				IsWinner = false,
// 				// PlayerId = PlayersFactory.Player1Id
// 			},
// 			new ()
// 			{
// 				Score = Player2Game2Score,
// 				IsWinner = true,
// 				// PlayerId = PlayersFactory.Player2Id
// 			}
// 		};
// 	}
// }