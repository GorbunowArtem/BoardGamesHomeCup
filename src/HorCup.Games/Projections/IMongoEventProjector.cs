using Revo.Domain.Entities;
using Revo.Infrastructure.Projections;

namespace HorCup.Games.Projections
{
	public interface IMongoEventProjector<T> : IEntityEventProjector
		where T : IAggregateRoot
	{
	}
}