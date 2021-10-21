using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NEventStore;
using NEventStore.PollingClient;
using NEventStore.Serialization;
using NEventStore.Serialization.Json;

namespace HorCup.Games
{
	public class SqlEventStore : IEventStore
	{
		private readonly IEventPublisher _publisher;
		private readonly Dictionary<Guid, List<IEvent>> _inMemoryDb = new Dictionary<Guid, List<IEvent>>();
		private static IStoreEvents store;

		public SqlEventStore(IEventPublisher publisher)
		{
			_publisher = publisher;
			store = WireupEventStore();
		}

		public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
		{
			foreach (var @event in events)
			{
				using var stream = store.OpenStream(@event.Id, 0, int.MaxValue);
				stream.Add(new EventMessage { Body = @event });
				await _publisher.Publish(@event, cancellationToken);
				stream.CommitChanges(Guid.NewGuid());
			}
		}

		public Task<IEnumerable<IEvent>> Get(
			Guid aggregateId,
			int fromVersion,
			CancellationToken cancellationToken = default)
		{
				long checkpointToken = 0;
				var client = new PollingClient2(store.Advanced, commit =>
				{
					
					checkpointToken = commit.CheckpointToken;
					return PollingClient2.HandlingResult.MoveToNext;
				}, waitInterval: 3000);
				client.StartFrom();
				
			return Task.FromResult(Enumerable.Empty<IEvent>());
			// return Task.FromResult(events?.Where(x => x.Version > fromVersion) ?? new List<IEvent>());
		}


		private static IStoreEvents WireupEventStore()
		{
			var loggerFactory = LoggerFactory.Create(logging =>
			{
				logging
					.AddConsole()
					.AddDebug()
					.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
			});

			return Wireup.Init()
				.WithLoggerFactory(loggerFactory)
				.UsingMongoPersistence("mongodb://localhost:27017/EventStore", new DocumentObjectSerializer())
				.InitializeStorageEngine()
				.UsingJsonSerialization()
				.Compress()
				// .HookIntoPipelineUsing(new[] { new AuthorizationPipelineHook() })
				.Build();
		}
	}

	public class AuthorizationPipelineHook : PipelineHookBase
	{
		public override ICommit Select(ICommit committed)
		{
			// return null if the user isn't authorized to see this commit
			return committed;
		}

		public override void PostCommit(ICommit committed)
		{
			// anything to do after the commit has been persisted.
		}

		public override bool PreCommit(CommitAttempt attempt)
		{
			// Can easily do logging or other such activities here
			return true; // true == allow commit to continue, false = stop.
		}
	}

	public class MongoEventStore : IEventStore
	{
		private readonly IEventPublisher _publisher;
		private readonly IMongoCollection<Commit> _commits;

		public MongoEventStore(IEventPublisher publisher, IOptions<MongoDbOptions> options)
		{
			_commits = new MongoClient(options.Value.ConnectionString).GetDatabase("GamesApiWrite")
				.GetCollection<Commit>("Commits");
			_publisher = publisher;
		}

		public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
		{
			foreach (var @event in events)
			{
				await _commits.UpdateOneAsync(
					Builders<Commit>.Filter.Eq(c => c.Id, @event.Id),
					Builders<Commit>.Update.Set(c => c.Id, @event.Id)
						.Set(c => c.Event, @event), new UpdateOptions { IsUpsert = true }
					, cancellationToken: cancellationToken);
				await _publisher.Publish(@event, cancellationToken);
			}
		}

		public async Task<IEnumerable<IEvent>> Get(
			Guid aggregateId,
			int fromVersion,
			CancellationToken cancellationToken = default)
		{
			var commits = await _commits.Find(Builders<Commit>.Filter.Eq(c => c.Id, aggregateId))
				.ToListAsync(cancellationToken);

			return commits.Select(c => c.Event);
		}
	}
}