using System;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Games.Queries.GetDetails
{
	public record GetGameDetailsQuery(Guid Id): IRequest<GameDetailsViewModel>;
}