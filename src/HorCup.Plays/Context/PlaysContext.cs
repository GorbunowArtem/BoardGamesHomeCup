using HorCup.Plays.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HorCup.Plays.Context
{
	public class PlaysContext : IPlaysContext
	{
		private readonly IMongoDatabase _db;

		public PlaysContext(IOptions<MongoDbOptions> options)
		{
			var client = new MongoClient(options.Value.ConnectionString);

			_db = client.GetDatabase("HorCupPlays");
		}

		public IMongoCollection<Play> Plays => _db.GetCollection<Play>("Plays");
	}
}