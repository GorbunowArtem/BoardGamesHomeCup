using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Exceptions;
using HorCup.Presentation.Games;
using HorCup.Presentation.Games.Queries.GetById;
using HorCup.Presentation.ViewModels;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.Games.Queries
{
	public class GetGameByIdQueryHandlerTests: TestFixtureBase
	{
		private GetGameByIdQueryHandler _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new GetGameByIdQueryHandler(Context, NullLogger<GetGameByIdQueryHandler>.Instance, Mapper);
		}

		[Test]
		public async Task Handle_GameNotExists_ExceptionThrown()
		{
			var id = new Guid();

			await _sut.Invoking(handler => handler.Handle(new GetGameByIdQuery(id), CancellationToken.None))
				.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage($"Entity {nameof(Game)} with key {id.ToString()} was not found");
		}

		[Test]
		public async Task Handle_GameExists_GameViewModelReturned()
		{
			var game = Context.Games.First();

			var result = await _sut.Handle(new GetGameByIdQuery(game.Id), CancellationToken.None);

			result.Should()
				.BeOfType<GameDetailsViewModel>();
		}
	}
}