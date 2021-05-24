using HorCup.Infrastructure.Services.IdGenerator;
using HorCup.Plays.Commands.AddPlay;
using HorCup.Plays.Context;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace HorCup.Plays.Tests.Commands.AddPlay
{
	[TestFixture]
	public class AddPlayCommandHandlerTests: TestFixtureBase
	{
		private AddPlayCommandHandler _sut;

		[SetUp]
		public void SetUp()
		{
			var idGeneratorMock = new Mock<IIdGenerator>();

			_sut = new AddPlayCommandHandler(idGeneratorMock.Object, new PlaysContext(null),
				NullLogger<AddPlayCommandHandler>.Instance, Mapper, null);
		}
	}
}