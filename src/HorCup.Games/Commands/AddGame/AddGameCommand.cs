using System;
using MediatR;

namespace HorCup.Games.Commands.AddGame
{
	public record AddGameCommand: IRequest<Guid>
	{
		public string Title { get; set; }

		public int MaxPlayers { get; set; }
	
		public int MinPlayers { get; set; }

		public bool HasScores { get; set; }
	}
}