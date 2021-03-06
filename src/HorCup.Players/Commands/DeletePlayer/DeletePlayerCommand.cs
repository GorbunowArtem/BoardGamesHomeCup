using System;
using MediatR;

namespace HorCup.Players.Commands.DeletePlayer
{
	public record DeletePlayerCommand(Guid Id) : IRequest<Unit>;
}