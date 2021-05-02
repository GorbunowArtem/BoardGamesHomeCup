using HorCup.Games.Commands.AddGame;
using HorCup.Games.Commands.EditGame;

namespace HorCup.Games.Tests.Factory
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
				Title = title ?? GamesFactory.CreatedGameTitle,
				MaxPlayers = GamesFactory.CreatedGameMaxPlayers,
				MinPlayers = GamesFactory.CreatedGameMinPlayers
			};

		public EditGameCommand EditGameCommand => new(_factory.Game2Id, GamesFactory.UpdatedTitle,
			12, 5, false);
	}
}