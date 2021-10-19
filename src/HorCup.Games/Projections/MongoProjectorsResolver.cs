using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Revo.Infrastructure.Projections;

namespace HorCup.Games.Projections
{
	public class MongoProjectorsResolver: IMongoProjectorsResolver
	{
		private readonly IKernel _kernel;

		public MongoProjectorsResolver(IKernel kernel)
		{
			_kernel = kernel;
		}

		public IReadOnlyCollection<IEntityEventProjector> GetProjectors(Type aggregateType)
		{
			return _kernel.GetAll(
				typeof(IMongoEventProjector<>).MakeGenericType(aggregateType))
				.Cast<IEntityEventProjector>()
				.ToArray();
		}

		public IReadOnlyCollection<IEntityEventProjector> GetSyncProjectors(Type aggregateType)
		{
			return Array.Empty<IEntityEventProjector>();
		}

		public bool HasAnyProjectors(Type aggregateType)
		{
			var bindings = _kernel.GetBindings(
				typeof(IMongoEventProjector<>).MakeGenericType(aggregateType));
			return bindings.Any();
		}

		public bool HasAnySyncProjectors(Type aggregateType)
		{
			return false;
		}
	}
}