using HorCup.Presentation.Games.Commands.AddGame;
using HorCup.Presentation.Games.Commands.EditGame;

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
			new()
			{
				Title = title ?? GamesFactory.Game1Title,
				MaxPlayers = GamesFactory.Game1MaxPlayers,
				MinPlayers = GamesFactory.Game1MinPlayers
			};

		public EditGameCommand EditGameCommand => new(_factory.Game2Id, GamesFactory.UpdatedTitle,
			12, 5, false);
	}
}