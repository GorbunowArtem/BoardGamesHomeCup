using System;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Games.Queries.GetById
{
	public record GetGameByIdQuery(Guid Id): IRequest<GameDetailsViewModel>;
}