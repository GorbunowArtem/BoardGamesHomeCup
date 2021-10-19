using Ninject;
using Revo.Core.Types;
using Revo.Infrastructure.Projections;

namespace HorCup.Games.Projections
{
	public class MongoProjectorDiscovery : ProjectorDiscovery
	{
		public MongoProjectorDiscovery(ITypeExplorer typeExplorer, StandardKernel kernel) : base(typeExplorer, kernel,
			new[] { typeof(IMongoEventProjector<>) })
		{
		}
	}
}