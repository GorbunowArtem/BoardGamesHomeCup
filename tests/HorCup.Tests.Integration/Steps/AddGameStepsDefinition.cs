using System;
using System.Collections.Generic;
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
		private readonly List<Guid> _gameIds;

		public AddGameStepsDefinition(WebDriver webDriver)
		{
			_webDriver = webDriver;
			_gameIds = new List<Guid>();
		}

		[When(@"I am adding new Game:")]
		public async Task WhenIAmAddingNewGame(Table table)
		{
			var games = table.CreateSet<AddGameCommand>();
			
			foreach (var game in games)
			{
				var response = await _webDriver.HttpPost("https://localhost:5002/games", new AddGameCommand
				{
					Title = $"{game.Title}{Guid.NewGuid().ToString()}",
					MaxPlayers = game.MaxPlayers,
					MinPlayers = game.MinPlayers
				});
			
				_gameIds.Add(new Guid(response));	
			}
		}

		[Then(@"new Game added")]
		public void ThenNewGameAdded()
		{
			_webDriver.CheckResponseStatusCode((int)HttpStatusCode.Created);
			_gameIds.ForEach(i => i.Should().NotBe(Guid.Empty));;
		}


		[Given(@"I am logged as Administrator")]
		public void GivenIAmLoggedAsAdministrator()
		{
		}
	}
}