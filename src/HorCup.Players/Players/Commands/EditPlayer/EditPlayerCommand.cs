using System;
using MediatR;

namespace HorCup.Players.Players.Commands.EditPlayer
{
	public record EditPlayerCommand(
		Guid Id,
		string Nickname) : IRequest<Unit>;
}