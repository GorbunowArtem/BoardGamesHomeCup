using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Plays.Queries.SearchPlays;
using HorCup.Presentation.ViewModels;
using HorCup.Tests.Games.Factory;
using HorCup.Tests.Plays.Factory;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace HorCup.Tests.Plays.Queries
{
	public class SearchPlaysQueryHandlerTests : TestFixtureBase
	{
		private SearchPlaysQueryHandler _sut;
		private readonly PlaysFactory _factory = new();

		[SetUp]
		public void SetUp()
		{
			_sut = new SearchPlaysQueryHandler(Context, NullLogger<SearchPlaysQueryHandler>.Instance);
		}

		[TestCase(new[] {111, 222}, new[] {14, 15})]
		[TestCase(new[] {55555}, new[] {17})]
		[TestCase(new int[0], new[] {13, 14, 15, 16, 17})]
		public async Task SearchByPlayerIds(int[] playerIds, int[] resultIds)
		{
			var result = await _sut.Handle(new SearchPlaysQuery
			{
				PlayersIds = playerIds.ToGuidArray()
			}, default);

			result.items.Select(p => p.Id)
				.Should()
				.BeEquivalentTo(resultIds.ToGuidArray());
		}

		[TestCase(new[] {2, 3}, new[] {14, 15})]
		[TestCase(new[] {4}, new[] {16, 17})]
		[TestCase(new int[0], new[] {13, 14, 15, 16, 17})]
		public async Task SearchByGameIds(int[] gameIds, int[] resultIds)
		{
			var result = await _sut.Handle(new SearchPlaysQuery
			{
				GamesIds = gameIds.ToGuidArray()
			}, default);

			result.items.Select(p => p.Id)
				.Should()
				.BeEquivalentTo(resultIds.ToGuidArray());
		}

		[TestCase(null, null, new[] {13, 14, 15, 16, 17})]
		[TestCase("2020-4-3", null, new[] {16, 17})]
		[TestCase("2020-4-4", null, new[] {16, 17})]
		[TestCase(null, "2020-3-3", new[] {13, 14, 15})]
		[TestCase(null, "2020-3-2", new[] {13, 14})]
		[TestCase("2020-2-2", "2020-3-2", new[] {14})]
		[TestCase("2020-2-2", "2020-3-3", new[] {14, 15})]
		public async Task SearchByDates(
			DateTime? dateFrom,
			DateTime? dateTo,
			int[] resultIds)
		{
			var result = await _sut.Handle(new SearchPlaysQuery
			{
				DateFrom = dateFrom,
				DateTo = dateTo
			}, CancellationToken.None);

			result.items.Select(p => p.Id)
				.Should()
				.BeEquivalentTo(resultIds.ToGuidArray());
		}

		[TestCase("Game 4", new[] {16, 17})]
		public async Task SearchByText(string searchText, int[] resultIds)
		{
			var (items, _) = await _sut.Handle(new SearchPlaysQuery
			{
				SearchText = searchText
			}, default);

			items.Select(p => p.Id)
				.Should()
				.BeEquivalentTo(resultIds.ToGuidArray());
		}

		[Test]
		public async Task SearchWithAllParameters()
		{
			var (items, _) = await _sut.Handle(new SearchPlaysQuery
			{
				DateFrom = TestExtensions.ToDateTime(2020, 3, 4),
				DateTo = TestExtensions.ToDateTime(2020, 5, 4),
				GamesIds = new[] {_factory.Games.Game4Id},
				PlayersIds = new[] {22.Guid()}
			}, default);

			items.First()
				.Id
				.Should()
				.Be(_factory.Play4Id);
		}

		[Test]
		public async Task Search_TakeAndSkip()
		{
			var (items, total) = await _sut.Handle(new SearchPlaysQuery
			{
				Skip = 2,
				Take = 1
			}, CancellationToken.None);

			total.Should().Be(5);

			var play = items.First();

			play.Should()
				.BeOfType<PlayViewModel>();
			play.GameId.Should().Be(_factory.Games.Game3Id);
			play.GameTitle.Should().Be(GamesFactory.Game3Title);
			play.PlayedDate.Should().Be(_factory.Play3Date);
		}

		[Test]
		public async Task Search_OrderByDatePlayed()
		{
			var (items, total) = await _sut.Handle(new SearchPlaysQuery(), CancellationToken.None);

			var play = items.First();

			play.Id.Should().Be(_factory.Play5Id);
		}
	}
}