using System;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Players.Queries.GetById
{
	public class GetPlayerByIdQuery: IRequest<PlayerDetailsViewModel>
	{
		public GetPlayerByIdQuery(Guid id)
		{
			Id = id;
		}
		
		public Guid Id { get; }
	}
}