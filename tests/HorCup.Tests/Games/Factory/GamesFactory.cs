using System;
using HorCup.Presentation.Games;

namespace HorCup.Tests.Games.Factory
{
	public class GamesFactory
	{
		public readonly Guid Game1Id = 1.Guid();
		public readonly Guid Game2Id = 2.Guid();
		public readonly Guid Game3Id = 3.Guid();
		public readonly Guid Game4Id = 4.Guid();
		
		public const string Game1Title = "Game 1";
		public const string Game2Title = "Game 2";
		public const string Game3Title = "Game 3";
		public const string Game4Title = "Game 4";
		public const string UpdatedTitle = "Updated title";
		public const string NotUniqueGameTitle = "Not unique";
		
		public const int Game1MinPlayers = 2;
		public const int Game2MinPlayers = 3;
		public const int Game3MinPlayers = 4;
		public const int Game4MinPlayers = 5;

		public const int Game1MaxPlayers = 4;
		public const int Game2MaxPlayers = 5;
		public const int Game3MaxPlayers = 6;
		public const int Game4MaxPlayers = 8;

		public Game[] Games => new[]
		{
			new Game
			{
				Id = Game2Id,
				Title = Game2Title,
				MinPlayers = Game2MinPlayers,
				MaxPlayers = Game2MaxPlayers
			},
			new Game
			{
				Id = Game3Id,
				Title = Game3Title,
				MinPlayers = Game3MinPlayers,
				MaxPlayers = Game3MaxPlayers
			},
			new Game
			{
				Id = Game4Id,
				Title = Game4Title,
				MinPlayers = Game4MinPlayers,
				MaxPlayers = Game4MaxPlayers
			}
		};

		public Commands Commands => new Commands(this);
	}
}