using System;
using Revo.Core.Commands;

namespace HorCup.Games.Queries.GetById
{
	public class GetGameByIdQuery : IQuery<GameReadModel>
	{
		public GetGameByIdQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}