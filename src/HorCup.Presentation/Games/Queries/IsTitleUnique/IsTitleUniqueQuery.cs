using System;
using MediatR;

namespace HorCup.Presentation.Games.Queries.IsTitleUnique
{
	public record IsTitleUniqueQuery(string Title, Guid? Id = null) : IRequest<bool>;
}