using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Tests.Integration.Drivers;
using TechTalk.SpecFlow;

namespace HorCup.Tests.Integration.Steps
{
	[Binding]
	public class AddPlayerStepsDefinition
	{
		private readonly WebDriver _driver;
		private Guid _playerId;
		
		public AddPlayerStepsDefinition(WebDriver driver)
		{
			_driver = driver;
		}

		[Given(@"I am logged as Admin")]
		public void GivenIAmLoggedAsAdmin()
		{
		}

		[When(@"I am filling unique nickname")]
		public async Task WhenIAmFillingUniqueNickname()
		{
			var response = await _driver.PostAsync("players", new AddPlayerCommand
			{
				Added = DateTime.Now,
				Nickname = $"Integ {Guid.NewGuid().ToString()}"[..19]
			});

			_playerId = new Guid(response);
		}

		[Then(@"new Player created")]
		public void ThenNewPlayerCreated()
		{
			_driver.CheckResponseStatusCode((int) HttpStatusCode.Created);
			_playerId.Should().NotBe(Guid.Empty);
		}
	}
}