using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akkatecture.Akka;
using HorCup.Games.Models;

namespace HorCup.Games.Queries
{
	public class GamesQueryHandler : IGamesQueryHandler
	{
		private readonly ActorRefProvider<GameStorageHandler> _gameStorageHandler;

		public GamesQueryHandler(ActorRefProvider<GameStorageHandler> gameStorageHandler)
		{
			_gameStorageHandler = gameStorageHandler;
		}

		public async Task<GameProjection> Get(Guid id)
		{
			var query = new GetGamesQuery();

			var result = await _gameStorageHandler.Ask<List<GameProjection>>(query);

			var model = result.SingleOrDefault(g => g.Id == id);

			return model;
		}
	}
}