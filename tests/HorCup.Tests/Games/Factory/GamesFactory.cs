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

		public const int Game1MaxPlayers = 6;

		public Game[] Games => new[]
		{
			new Game
			{
				Title = Game2Title,
				MaxPlayers = Game1MaxPlayers
			},
			new Game
			{
				Title = Game3Title,
				MaxPlayers = Game1MaxPlayers
			},
			new Game
			{
				Title = Game4Title,
				MaxPlayers = Game1MaxPlayers
			}
		};

		public Commands Commands => new Commands(this);
	}
}