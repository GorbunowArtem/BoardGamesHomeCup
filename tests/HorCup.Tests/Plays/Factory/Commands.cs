using HorCup.Presentation.Plays.Commands.AddPlay;

namespace HorCup.Tests.Plays.Factory
{
	public class Commands
	{
		private readonly PlaysFactory _factory;
		
		public Commands(PlaysFactory factory)
		{
			_factory = factory;
		}

		public AddPlayCommand AddPlayCommand => new()
		{
			GameId = _factory.Games.Game2Id,
			Notes = PlaysFactory.Notes1,
			PlayedDate = TestExtensions.ToDateTime(2020, 12, 11),
			PlayerScores = PlaysFactory.Play1Scores
		};
	}
}