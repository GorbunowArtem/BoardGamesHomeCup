using Revo.Core.Commands;
using Revo.Core.Transactions;
using Revo.Infrastructure.Events.Async;
using Revo.Infrastructure.Projections;

namespace HorCup.Games.Projections
{
	public class MongoProjectionEventListener     : ProjectionEventListener
	{
		public MongoProjectionEventListener(IProjectionSubSystem projectionSubSystem, IUnitOfWorkFactory unitOfWorkFactory,
			CommandContextStack commandContextStack) : base(projectionSubSystem, unitOfWorkFactory, commandContextStack)
		{
		}

		public override IAsyncEventSequencer EventSequencer { get; }
	}
}