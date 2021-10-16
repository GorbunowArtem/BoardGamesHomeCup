using System.Threading.Tasks;
using AutoMapper;
using HorCup.Plays.Context;
using HorCup.Plays.Models;
using MassTransit;
using Moq;

namespace HorCup.Plays.Tests
{
	public class TestFixtureBase
	{
		protected IMapper Mapper { get; }

		protected Mock<IPlaysContext> Context { get; }

		protected Mock<IPublishEndpoint> PublishEndpoint { get; }

		public TestFixtureBase()
		{
			var configProvider = new MapperConfiguration(cfg => { cfg.AddProfile<PlaysProfile>(); });

			Mapper = configProvider.CreateMapper();

			Context = new Mock<IPlaysContext>();
			Context.Setup(c => c.Plays.InsertOneAsync(It.IsAny<Play>(), null, default))
				.Returns(Task.CompletedTask);

			PublishEndpoint = new Mock<IPublishEndpoint>();
			
		}                     
	}
}