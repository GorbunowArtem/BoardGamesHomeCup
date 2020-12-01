using System;
using HorCup.Presentation.Games;

namespace HorCup.Tests.Games.Factory
{
	public class GamesFactory
	{
		public readonly Guid Game1Id = new Guid("54E259FB-885A-45A3-B28D-46118ECA464B");
		
		public const string Game1Title = "Game 1";
		public const string Game2Title = "Game 2";
		public const string Game3Title = "Game 3";
		public const string Game4Title = "Game 4";
		public const string NotUniqueGameTitle = "Game 4";

		public const int Game1MaxPlayers = 4;
		public const int Game2MaxPlayers = 6;
		public const int Game3MaxPlayers = 5;
		public const int Game4MaxPlayers = 8;
		
		public const int Game1MinPlayers = 2;
		public const int Game2MinPlayers = 2;
		public const int Game3MinPlayers = 3;
		public const int Game4MinPlayers = 4;

		public Game[] Games => new[]
		{
			new Game
			{
				Title = Game2Title,
				MinPlayers = Game2MinPlayers,
				MaxPlayers = Game2MaxPlayers
			},
			new Game
			{
				Title = Game3Title,
				MinPlayers = Game3MinPlayers,
				MaxPlayers = Game3MaxPlayers
			},
			new Game
			{
				Title = Game4Title,
				MinPlayers = Game4MinPlayers,
				MaxPlayers = Game4MaxPlayers
			}
		};

		public Commands Commands => new Commands(this);
	}
}