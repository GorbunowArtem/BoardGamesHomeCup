using TechTalk.SpecFlow;

namespace HorCup.Tests.Integration.Steps
{
	[Binding]
	public class AddPlayerStepsDefinition
	{
		[Given(@"I am logged as Administrator")]
		public void GivenIAmLoggedAsAdministrator()
		{
			ScenarioContext.StepIsPending();
		}

		[When(@"I am adding new PLayer with unique nickname")]
		public void WhenIAmAddingNewPLayerWithUniqueNickname()
		{
			ScenarioContext.StepIsPending();
		}

		[Then(@"new Player created")]
		public void ThenNewPlayerCreated()
		{
			ScenarioContext.StepIsPending();
		}
	}
}