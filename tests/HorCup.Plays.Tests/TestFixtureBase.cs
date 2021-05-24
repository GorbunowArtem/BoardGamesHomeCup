using AutoMapper;

namespace HorCup.Plays.Tests
{
	public class TestFixtureBase
	{
		protected IMapper Mapper { get; }

		public TestFixtureBase()
		{
			var configProvider = new MapperConfiguration(cfg => { cfg.AddProfile<PlaysProfile>(); });

			Mapper = configProvider.CreateMapper();
		}
	}
}