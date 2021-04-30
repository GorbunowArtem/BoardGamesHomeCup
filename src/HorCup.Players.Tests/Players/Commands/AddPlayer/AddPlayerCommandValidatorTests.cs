using FluentValidation.TestHelper;
using HorCup.Players.Players.Commands.AddPlayer;
using NUnit.Framework;

namespace HorCup.Players.Tests.Players.Commands.AddPlayer
{
	[TestFixture]
	public class AddPlayerCommandValidatorTests
	{
		private AddPlayerCommandValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_validator = new AddPlayerCommandValidator();
		}

		[TestCase(null, "'Nickname' must not be empty.")]
		[TestCase("", "'Nickname' must not be empty.")]
		[TestCase("More than max length.",
			"The length of 'Nickname' must be 20 characters or fewer. You entered 21 characters.")]
		public void AddPlayerCommandValidator_NicknameIsInvalid_ValidationErrorThrown(string name, string errorMessage)
		{
			var model = new AddPlayerCommand
			{
				Nickname = name
			};

			var result = _validator.TestValidate(model);

			result.ShouldHaveValidationErrorFor(p => p.Nickname)
				.WithErrorMessage(errorMessage);
		}

		[TestCase("Less than max length")]
		[TestCase("Y")]
		public void AddPlayerCommandValidator_NicknameIsValid_ValidationPassed(string name)
		{
			var model = new AddPlayerCommand
			{
				Nickname = name
			};

			var result = _validator.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(p => p.Nickname);
		}
	}
}