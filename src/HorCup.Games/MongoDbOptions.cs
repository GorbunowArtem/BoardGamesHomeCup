namespace HorCup.Games
{
	public class MongoDbOptions
	{
		public const string MongoDb = "MongoDb";

		public string ConnectionString { get; set; }
		
		public string DbName { get; set; }
	}
}