using System;
using FluentValidation;
using FluentValidation.TestHelper;
using HorCup.Presentation.Plays.Commands.AddPlay;
using NUnit.Framework;

namespace HorCup.Tests.Plays.Commands.AddPlay
{
	[TestFixture]
	public class AddPlayCommandValidatorTests
	{
		private AddPlayCommandValidator _validator;

		[SetUp]
		public void SetUp()
		{
			_validator = new AddPlayCommandValidator();
		}
		
		[Test]
		public void AddPlayCommandValidator_GameIdIsEmpty_ValidationErrorThrown()
		{
			var model = new AddPlayCommand
			{
				GameId = Guid.Empty
			};

			var result = _validator.TestValidate(model);

			result.ShouldHaveValidationErrorFor(p => p.GameId)
				.WithErrorMessage("'Game Id' must not be equal to '00000000-0000-0000-0000-000000000000'.");
		}

		[Test]
		public void AddPlayCommandValidator_GameIdIsNotEmpty_ValidationPassed()
		{
			var model = new AddPlayCommand
			{
				GameId = 12.Guid()
			};

			var result = _validator.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(p => p.GameId);
		}
		
	}
}