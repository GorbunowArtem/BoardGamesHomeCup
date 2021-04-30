using System;
using MediatR;

namespace HorCup.Players.Commands.EditPlayer
{
	public record EditPlayerCommand(
		Guid Id,
		string Nickname) : IRequest<Unit>;
}