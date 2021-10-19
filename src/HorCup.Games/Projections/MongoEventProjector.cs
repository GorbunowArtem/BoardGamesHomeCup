using Revo.Domain.Entities;
using Revo.Infrastructure.Projections;

namespace HorCup.Games.Projections
{
	public abstract class MongoEventProjector<T> : EntityEventProjector, IMongoEventProjector<T> where T : class, IAggregateRoot
	{
		
	}
}