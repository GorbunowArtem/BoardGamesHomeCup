using System.ComponentModel;
using TechTalk.SpecFlow;

namespace HorCup.Tests.Integration.Steps
{
	[Binding]
	public class AddGameStepsDefinition
	{
		[When(@"I am adding new Game with title (.*), minimum players (.*) and maximum players (.*)")]
		public void WhenIAmAddingNewGameWithTitleGameMinimumPlayersAndMaximumPlayers(string title, int minPlayers,
			string maxPlayers)
		{
		}

		[Then(@"new Game created")]
		public void ThenNewGameCreated()
		{
		}
	}
}