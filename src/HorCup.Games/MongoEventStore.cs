using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HorCup.Games
{
	public class MongoEventStore : IEventStore
	{
		private readonly IEventPublisher _publisher;
		private readonly IMongoCollection<Commit> _commits;

		public MongoEventStore(IEventPublisher publisher, IOptions<MongoDbOptions> options)
		{
			_commits = new MongoClient(options.Value.ConnectionString).GetDatabase("GamesApiWrite").GetCollection<Commit>("Commits");
			_publisher = publisher;
		}

		public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
		{
			foreach (var @event in events)
			{
				await _commits.InsertOneAsync(new Commit(@event), cancellationToken: cancellationToken);
				await _publisher.Publish(@event, cancellationToken);
			}
		}

		public async Task<IEnumerable<IEvent>> Get(
			Guid aggregateId,
			int fromVersion,
			CancellationToken cancellationToken = default)
		{
			var commits = await _commits.Find(Builders<Commit>.Filter.Eq("Event.Id", aggregateId))
				.ToListAsync(cancellationToken);

			return commits.Select(c => c.Event);
		}
	}
}