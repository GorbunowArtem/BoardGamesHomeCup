using System;
using System.Collections.Generic;
using Revo.Infrastructure.Projections;

namespace HorCup.Games.Projections
{
	public interface IMongoProjectorsResolver
	{
		IReadOnlyCollection<IEntityEventProjector> GetProjectors(Type aggregateType);
		IReadOnlyCollection<IEntityEventProjector> GetSyncProjectors(Type aggregateType);
		bool HasAnyProjectors(Type aggregateType);
		bool HasAnySyncProjectors(Type aggregateType);
	}
}