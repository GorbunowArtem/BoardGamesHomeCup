using System;
using MediatR;

namespace HorCup.Games.Commands.AddGame
{
	public record AddGameCommand(
		string Title,
		int MaxPlayers,
		int MinPlayers) : IRequest<Guid>;
}