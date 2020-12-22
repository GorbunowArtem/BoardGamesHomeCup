using System;
using MediatR;

namespace HorCup.Presentation.Games.Commands.DeleteGame
{
	public record DeleteGameCommand(Guid Id) : IRequest<Unit>;
}