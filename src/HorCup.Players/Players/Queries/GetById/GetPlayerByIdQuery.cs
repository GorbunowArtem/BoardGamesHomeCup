using System;
using HorCup.Players.Shared.ViewModels;
using MediatR;
using PlayerDetailsViewModel = HorCup.Players.ViewModels.PlayerDetailsViewModel;

namespace HorCup.Players.Players.Queries.GetById
{
	public record GetPlayerByIdQuery(Guid Id) : IRequest<PlayerDetailsViewModel>;
}