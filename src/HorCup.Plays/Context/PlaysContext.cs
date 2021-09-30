using HorCup.Plays.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HorCup.Plays.Context
{
	public class PlaysContext : IPlaysContext
	{
		private readonly IMongoDatabase _db;
		private readonly ILogger<PlaysContext> _logger;

		public PlaysContext(IOptions<MongoDbOptions> options, ILogger<PlaysContext> logger)
		{
			_logger = logger;
			_logger.LogInformation($"Connection string is {options.Value.ConnectionString}");
			var client = new MongoClient(options.Value.ConnectionString);

			_db = client.GetDatabase("HorCupPlays");
		}

		public IMongoCollection<Play> Plays => _db.GetCollection<Play>("Plays");
	}
}