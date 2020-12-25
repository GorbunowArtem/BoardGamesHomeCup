using System;
using MediatR;

namespace HorCup.Presentation.Games.Commands.EditGame
{
	public record EditGameCommand(Guid Id, string Title, int MaxPlayers, int MinPlayers, bool HasScores) : IRequest<Unit>;
}