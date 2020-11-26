using HorCup.Presentation.Players.Commands.AddPlayer;

namespace HorCup.Tests.Players.Factory
{
	public class Commands
	{
		private readonly PlayersFactory _factory;

		public Commands(PlayersFactory factory)
		{
			_factory = factory;
		}

		public AddPlayerCommand AddPlayer(string nickname = null) =>
			new AddPlayerCommand
			{
				BirthDate = _factory.Player3BirthDate,
				FirstName = PlayersFactory.Player3FirstName,
				LastName = PlayersFactory.Player3LastName,
				Nickname = nickname ?? PlayersFactory.Player3NickName
			};
	}
}