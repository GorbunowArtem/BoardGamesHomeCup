using System;
using HorCup.Presentation.Players;

namespace HorCup.Tests.Players.Factory
{
	public class PlayersFactory
	{
		public readonly Guid Player1Id = 431.Guid();
		public const string Player1FirstName = "Player 1 First";
		public const string Player1LastName = "Player 1 Last";
		public const string Player1NickName = "Player1Nick";

		public readonly Guid Player2Id = 567.Guid();
		public const string Player2FirstName = "Player 2 First";
		public const string Player2LastName = "Player 2 Last";
		public const string Player2NickName = "Player2Nick";

		public const string Player3FirstName = "   Player 3 First   ";
		public const string Player3LastName = "   Player 3 Last   ";
		public const string Player3NickName = "   Player3Nick  ";

		public readonly DateTime Player1BirthDate = new DateTime(1990, 2, 3);
		public readonly DateTime Player2BirthDate = new DateTime(1991, 3, 4);
		public readonly DateTime Player3BirthDate = new DateTime(1992, 4, 5);

		public Player[] Players => new[]
		{
			new Player
			{
				Id = Player1Id,
				FirstName = Player1FirstName,
				LastName = Player1LastName,
				Nickname = Player1NickName,
				BirthDate = Player1BirthDate
			}, 
			
			new Player
			{
				Id = Player2Id,
				FirstName = Player2FirstName,
				LastName = Player2LastName,
				Nickname = Player2NickName,
				BirthDate = Player2BirthDate
			}, 
		};
		
		public Commands Commands => new Commands(this);
	}
}