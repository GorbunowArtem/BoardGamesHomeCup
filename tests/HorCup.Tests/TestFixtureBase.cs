using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HorCup.Presentation;
using HorCup.Presentation.Context;

namespace HorCup.Tests
{
	public abstract class TestFixtureBase : IDisposable
	{
		protected readonly HorCupContext Context;

		protected TestFixtureBase()
		{
			Context = HorCupContextFactory.Create();

			var configProvider = new MapperConfiguration(cfg => { cfg.AddProfile<HorCupProfile>(); });

			Mapper = configProvider.CreateMapper();
		}

		public void Dispose()
		{
			HorCupContextFactory.Destroy(Context);
		}

		protected IMapper Mapper { get; }

		protected static IEnumerable<Guid> ToGuidList(IEnumerable<int> ids) =>
			ids.Select(i => i.Guid()).ToArray();
	}
}