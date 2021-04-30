using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Games.Commands.AddGame;
using HorCup.Games.Models;
using HorCup.Games.ViewModels;
using HorCup.Presentation.Requests;
using HorCup.Tests.Integration.Drivers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HorCup.Tests.Integration.Steps
{
	[Binding]
	public class GameStepsDefinition
	{
		private const string GameEndpoint = "games";
		private readonly WebDriver _webDriver;
		private Guid _gameId;
		private readonly EditGameRequest _updateRequest;

		public GameStepsDefinition(WebDriver webDriver)
		{
			_webDriver = webDriver;
			_updateRequest = new EditGameRequest($"Updated{Guid.NewGuid().ToString()}", 6, 3, false);
		}

		[When(@"I am filling title, min and max players:")]
		public async Task WhenIAmFillingTitleMinAndMaxPlayers(Table table)
		{
			var command = table.CreateInstance<AddGameCommand>();

			command.Title = $"{command.Title}{Guid.NewGuid().ToString()}";

			var response = await _webDriver.PostAsync(GameEndpoint, command);

			_gameId = new Guid(response);
		}

		[Then(@"new Game added")]
		public void ThenNewGameAdded()
		{
			_webDriver.CheckResponseStatusCode((int) HttpStatusCode.Created);
			_gameId.Should().NotBe(Guid.Empty);
		}

		[When(@"I change Game title, min and max players")]
		public async Task WhenIChangeGameTitleMinAndMaxPlayers()
		{
			await _webDriver.PatchAsync($"{GameEndpoint}/{_gameId.ToString()}", _updateRequest);
		}

		[Then(@"Game is updated")]
		public async Task ThenGameIsUpdated()
		{
			_webDriver.CheckResponseStatusCode((int) HttpStatusCode.NoContent);

			var updatedGame = await _webDriver.GetAsync<GameDetailsViewModel>($"{GameEndpoint}/{_gameId.ToString()}");

			updatedGame.Title.Should().Be(_updateRequest.Title);
			updatedGame.MinPlayers.Should().Be(_updateRequest.MinPlayers);
			updatedGame.MaxPlayers.Should().Be(_updateRequest.MaxPlayers);
		}

		[When(@"I delete Game")]
		public async Task WhenIDeleteGame()
		{
			await _webDriver.DeleteAsync($"{GameEndpoint}/{_gameId.ToString()}");
		}

		[Then(@"Game no longer exists in system")]
		public async Task ThenGameNoLongerExistsInSystem()
		{
			_webDriver.CheckResponseStatusCode((int) HttpStatusCode.NoContent);

			var game = await _webDriver.GetAsync<Game>($"{GameEndpoint}/{_gameId.ToString()}");

			_webDriver.CheckResponseStatusCode((int) HttpStatusCode.NotFound);
			game.Should().BeNull();
		}
	}
}