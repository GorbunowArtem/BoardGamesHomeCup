using HorCup.Games.Events;
using HorCup.Games.Models;
using HorCup.Games.Projections;
using Microsoft.Extensions.Logging;
using Revo.Core.Events;
using Revo.EFCore.DataAccess.Entities;
using Revo.EFCore.Projections;
using Revo.Infrastructure.Projections;

namespace HorCup.Games.Queries
{
	public class GamesProjector: MongoEventProjector<GameAggregate>
	{
		private void Apply(IEventMessage<GameTitleSet> evt)
		{
			var game = new GameReadModel
			{
				Id = evt.Event.AggregateId,
				Title = evt.Event.Title
			};
			
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
	
	}	
	
	
}