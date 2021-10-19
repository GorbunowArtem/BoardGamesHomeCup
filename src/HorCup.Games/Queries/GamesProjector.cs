using HorCup.Games.Events;
using HorCup.Games.Models;
using Revo.Core.Events;
using Revo.EFCore.DataAccess.Entities;
using Revo.EFCore.Projections;
using Revo.Infrastructure.Repositories;
using Revo.RavenDB.DataAccess;
using Revo.RavenDB.Projections;

namespace HorCup.Games.Queries
{
	public class GamesProjector: RavenEntityEventToPocoProjector<GameAggregate, GameReadModel>
	{
		//
		private void Apply(IEventMessage<GameTitleSet> evt)
		{
			var game = new GameReadModel
			{
				Id = evt.Event.AggregateId,
				Title = evt.Event.Title
			};
			
			Repository.Add(game);
		}
		// 	Target.Title = evt.Event.Title;
		// }
		//
		// private void Apply(IEventMessage<GamePlayersNumberChanged> evt)
		// {
		// 	Target.MinPlayers = evt.Event.MinPlayers;
		// 	Target.MaxPlayers = evt.Event.MaxPlayers;
		// }
		// private void Apply(IEventMessage<GameDescriptionChanged> evt)
		// {
		// 	Target.Description = evt.Event.Description;
		// }


		public GamesProjector(IRavenCrudRepository repository) : base(repository)
		{
		}
	}
}