using HorCup.Presentation.Games.Commands.AddGame;

namespace HorCup.Tests.Games.Factory
{
	public class Commands
	{
		private readonly GamesFactory _factory;

		public Commands(GamesFactory factory)
		{
			_factory = factory;
		}

		public AddGameCommand AddGameCommand(string title = null) =>
			new AddGameCommand
			{
				Title = title ?? GamesFactory.Game1Title,
				MaxPlayers = GamesFactory.Game1MaxPlayers,
				MinPlayers = GamesFactory.Game1MinPlayers
			};
	}
}