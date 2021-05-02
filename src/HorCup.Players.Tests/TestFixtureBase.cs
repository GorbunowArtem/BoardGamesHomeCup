using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HorCup.Players.Context;

namespace HorCup.Players.Tests
{
	public abstract class TestFixtureBase: IDisposable
	{
		protected readonly PlayersContext Context;

		protected TestFixtureBase()
		{
			Context = PlayersContextFactory.Create();

			var configProvider = new MapperConfiguration(cfg => { cfg.AddProfile<PlayersProfile>(); });

			Mapper = configProvider.CreateMapper();
		}

		public void Dispose()
		{
			PlayersContextFactory.Destroy(Context);
		}

		protected IMapper Mapper { get; }

		protected static IEnumerable<Guid> ToGuidList(IEnumerable<int> ids) =>
			ids.Select(i => i.Guid()).ToArray();
	}
}