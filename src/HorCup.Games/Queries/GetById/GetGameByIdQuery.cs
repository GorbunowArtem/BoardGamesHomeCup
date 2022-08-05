using System;
using HorCup.Games.ViewModels;
using MediatR;

namespace HorCup.Games.Queries.GetById;

public record GetGameByIdQuery(Guid Id): IRequest<GameDetailsViewModel>;