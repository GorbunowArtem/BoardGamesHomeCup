using System;
using HorCup.Presentation.Players.Commands.AddPlayer;

namespace HorCup.Tests.Players.Factory
{
	public class PlayersFactory
	{
		public const string Player1FirstName = "Player 1 First";
		public const string Player1LastName = "Player 1 Last";
		public const string Player1NickName = "Player1Nick";

		public readonly Guid Player1Id = new Guid("8B7C5165-B72E-4BDD-A5F1-BACE9D2A6541");
		public readonly DateTime Player1BirthDate = new DateTime(1990, 2, 3);

		public Commands Commands => new Commands(this);
	}

	public class Commands
	{
		private readonly PlayersFactory _factory;

		public Commands(PlayersFactory factory)
		{
			_factory = factory;
		}

		public AddPlayerCommand AddPlayer() =>
			new AddPlayerCommand
			{
				BirthDate = _factory.Player1BirthDate,
				FirstName = PlayersFactory.Player1FirstName,
				LastName = PlayersFactory.Player1LastName,
				Nickname = PlayersFactory.Player1NickName
			};
	}
}