using System.Security.Authentication;
using HorCup.Plays.PlayScores;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HorCup.Plays.Context
{
	public class PlaysContext : IPlaysContext
	{
		private readonly IMongoDatabase _db;

		public PlaysContext()
		{
			var connString =
				"mongodb://localhost:C2y6yDjf5%2FR%2Bob0N8A7Cgv30VRDJIWEHLM%2B4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw%2FJw%3D%3D@localhost:10255/admin?ssl=true";
			var settings = MongoClientSettings.FromUrl(new MongoUrl(connString));
			settings.SslSettings = new SslSettings
			{
				EnabledSslProtocols = SslProtocols.Tls12
			};
			
			
			
			// var client = new MongoClient("mongodb://localhost:C2y6yDjf5%2FR%2Bob0N8A7Cgv30VRDJIWEHLM%2B4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw%2FJw%3D%3D@localhost:10255/admin?ssl=true");
			var client = new MongoClient(settings);

			_db = client.GetDatabase("Plays");

		}

		public IMongoCollection<Play> Plays => _db.GetCollection<Play>("Play");
	}
}