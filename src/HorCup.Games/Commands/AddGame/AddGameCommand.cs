using Akkatecture.Commands;
using HorCup.Games.Models;

namespace HorCup.Games.Commands.AddGame
{
	public class AddGameCommand : Command<GameAggregate, GameId>
	{
		public AddGameCommand(
			GameId aggregateId,
			string title,
			int maxPlayers,
			int minPlayers) : base(aggregateId)
		{
			Title = title;
			MaxPlayers = maxPlayers;
			MinPlayers = minPlayers;
		}

		public string Title { get; set; }
		public int MaxPlayers { get; set; }
		public int MinPlayers { get; set; }
	}
}