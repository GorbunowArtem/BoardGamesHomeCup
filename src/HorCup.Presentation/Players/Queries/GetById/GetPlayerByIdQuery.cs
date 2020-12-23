using System;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Players.Queries.GetById
{
	public record GetPlayerByIdQuery(Guid Id) : IRequest<PlayerDetailsViewModel>;
}