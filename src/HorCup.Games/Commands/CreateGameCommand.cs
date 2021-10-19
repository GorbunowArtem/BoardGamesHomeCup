using System;
using Revo.Core.Commands;

namespace HorCup.Games.Commands
{
	public class CreateGameCommand : ICommand
	{
		public CreateGameCommand(
			Guid id,
			string title,
			int minPlayers,
			int maxPlayers,
			string description)
		{
			Id = id;
			Title = title;
			MinPlayers = minPlayers;
			MaxPlayers = maxPlayers;
			Description = description;
		}

		public Guid Id { get; set; }
		
		public string Title { get; set; }

		public int MinPlayers { get; set; }

		public int MaxPlayers { get; set; }

		public string Description { get; set; }
	}
}