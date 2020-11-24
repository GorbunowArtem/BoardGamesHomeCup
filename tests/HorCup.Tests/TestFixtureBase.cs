using System;
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
	}
}