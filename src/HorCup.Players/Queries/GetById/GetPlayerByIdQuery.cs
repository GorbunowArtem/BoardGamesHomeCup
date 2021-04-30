using System;
using HorCup.Players.ViewModels;
using MediatR;

namespace HorCup.Players.Queries.GetById
{
	public record GetPlayerByIdQuery(Guid Id) : IRequest<PlayerDetailsViewModel>;
}