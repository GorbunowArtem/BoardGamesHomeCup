using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using HorCup.Players.Players.Commands.AddPlayer;
using HorCup.Players.Players.Commands.EditPlayer;
using HorCup.Players.Shared.ViewModels;
using HorCup.Presentation.ViewModels;
using HorCup.Tests.Integration.Drivers;
using TechTalk.SpecFlow;

namespace HorCup.Tests.Integration.Steps
{
	[Binding]
	public class PlayerStepsDefinition
	{
		private const string PlayersEndpoint = "players";
		private readonly WebDriver _driver;
		private Guid _playerId;
		
		public PlayerStepsDefinition(WebDriver driver)
		{
			_driver = driver;
		}

		[When(@"I am filling unique nickname")]
		public async Task WhenIAmFillingUniqueNickname()
		{
			var response = await _driver.PostAsync(PlayersEndpoint, new AddPlayerCommand
			{
				Added = DateTime.Now,
				Nickname = $"Integ {Guid.NewGuid().ToString()}"[..19]
			});

			_playerId = new Guid(response);
		}

		[Then(@"new Player added")]
		public void ThenNewPlayerCreated()
		{
			_driver.CheckResponseStatusCode((int) HttpStatusCode.Created);
			_playerId.Should().NotBe(Guid.Empty);
		}

		[When(@"I change Player nickname")]
		public async Task WhenIChangePlayerNickname()
		{
			await _driver.PatchAsync($"{PlayersEndpoint}/{_playerId.ToString()}",
				new EditPlayerCommand(_playerId, $"Updated {Guid.NewGuid().ToString()}"[..19]));
		}

		[Then(@"Player is updated")]
		public async Task ThenPlayerIsUpdated()
		{
			_driver.CheckResponseStatusCode((int) HttpStatusCode.NoContent);

			var updatedPlayer = await _driver.GetAsync<PlayerViewModel>($"{PlayersEndpoint}/{_playerId.ToString()}");

			updatedPlayer.Nickname.Should().Be(updatedPlayer.Nickname);
		}

		[When(@"I delete Player")]
		public async Task WhenIDeletePlayer()
		{
			await _driver.DeleteAsync($"{PlayersEndpoint}/{_playerId.ToString()}");
		}

		[Then(@"Player no longer exists in system")]
		public async Task ThenPlayerNoLongerExistsInSystem()
		{
			_driver.CheckResponseStatusCode((int) HttpStatusCode.NoContent);

			var player = await _driver.GetAsync<PlayerViewModel>($"{PlayersEndpoint}/{_playerId.ToString()}");

			player.Should().BeNull();
			_driver.CheckResponseStatusCode((int) HttpStatusCode.NotFound);
		}
	}
}