using System;
using MediatR;

namespace HorCup.Presentation.Players.Commands.DeletePlayer
{
	public record DeletePlayerCommand(Guid Id) : IRequest<Unit>;
}