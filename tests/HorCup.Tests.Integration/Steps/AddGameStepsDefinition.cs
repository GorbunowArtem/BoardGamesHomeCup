using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Games.Commands.AddGame;
using HorCup.Tests.Integration.Drivers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HorCup.Tests.Integration.Steps
{
	[Binding]
	public class AddGameStepsDefinition
	{
		private readonly WebDriver _webDriver;
		private Guid _gameId;

		public AddGameStepsDefinition(WebDriver webDriver)
		{
			_webDriver = webDriver;
		}

		[When(@"I am adding new Game:")]
		public async Task WhenIAmAddingNewGame(Table table)
		{
			var command = table.CreateInstance<AddGameCommand>();

			command.Title = $"{command.Title}{Guid.NewGuid().ToString()}";

			var response = await _webDriver.HttpPost("games", command);

			_gameId = new Guid(response);
		}

		[Then(@"new Game added")]
		public void ThenNewGameAdded()
		{
			_webDriver.CheckResponseStatusCode((int) HttpStatusCode.Created);
			_gameId.Should().NotBe(Guid.Empty);
		}
	}
}