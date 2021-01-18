using FluentValidation.TestHelper;
using HorCup.Presentation.Games.Commands.AddGame;
using NUnit.Framework;

namespace HorCup.Tests.Games.Commands.AddGame
{
	[TestFixture]
	public class AddGameValidatorTests
	{
		private AddGameCommandValidator _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new AddGameCommandValidator();
		}

		[TestCase(null ,"'Title' must not be empty.")]
		[TestCase("" ,"'Title' must not be empty.")]
		[TestCase("String with more symbols than described in GamesConstraints class", "The length of 'Title' must be 50 characters or fewer. You entered 65 characters.")]
		public void AddGameCommandValidator_TitleRules(string title, string errorMessage)
		{
			var model = new AddGameCommand
			{
				Title = title
			};
			
			var result = _sut.TestValidate(model);
			
			result.ShouldHaveValidationErrorFor(g => g.Title)
				.WithErrorMessage(errorMessage);
		}
	}
}