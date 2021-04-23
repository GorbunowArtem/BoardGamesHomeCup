using System;
using System.Text.Json;
using System.Threading.Tasks;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Tests.Integration.Drivers;
using TechTalk.SpecFlow;

namespace HorCup.Tests.Integration.Steps
{
	[Binding]
	public class AddPlayerStepsDefinition
	{
		private readonly WebDriver _driver;

		public AddPlayerStepsDefinition(WebDriver driver)
		{
			_driver = driver;
		}

		[Given(@"I am logged in")]
		public void GivenIAmLoggedAsAdministrator()
		{
		}

		[When(@"I am adding new Player with unique nickname")]
		public async Task WhenIAmAddingNewPlayerWithUniqueNickname()
		{
			var response = await _driver.HttpPost("https://localhost:5002/api/players", new AddPlayerCommand
			{
				Added = DateTime.Now,
				Nickname = $"IntegrationPlayer{Guid.NewGuid().ToString()}"
			});
			
			var id = JsonSerializer.Deserialize<Guid>(response);
		}

		[Then(@"new Player created")]
		public void ThenNewPlayerCreated()
		{
		}
	}
}