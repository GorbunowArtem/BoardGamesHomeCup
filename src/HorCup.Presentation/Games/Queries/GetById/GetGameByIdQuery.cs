using System;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Games.Queries.GetById
{
	public class GetGameByIdQuery: IRequest<GameDetailsViewModel>
	{
		public Guid Id { get; }

		public GetGameByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}