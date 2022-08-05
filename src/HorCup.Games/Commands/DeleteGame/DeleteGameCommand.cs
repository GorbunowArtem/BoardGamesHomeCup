using System;
using MediatR;

namespace HorCup.Games.Commands.DeleteGame;

public record DeleteGameCommand(Guid Id) : IRequest<Unit>;