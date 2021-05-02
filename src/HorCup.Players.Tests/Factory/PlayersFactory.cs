using System;
using HorCup.Players.Models;

namespace HorCup.Players.Tests.Factory
{
	public class PlayersFactory
	{
		public readonly Guid Player1Id = 431.Guid();
		public const string Player1NickName = "Player1Nick";

		public readonly Guid Player2Id = 567.Guid();
		public const string Player2NickName = "Player2Nick";

		public const string Player3NickName = "   Player3Nick  ";

		public readonly Guid CreatedPlayerId = 444.Guid();

		public Player[] Players => new[]
		{
			new Player
			{
				Id = Player1Id,
				Nickname = Player1NickName,
			},

			new Player
			{
				Id = Player2Id,
				Nickname = Player2NickName,
			},
		};

		public Commands Commands => new Commands(this);
	}
}