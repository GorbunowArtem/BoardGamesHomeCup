using System;
using FluentValidation.TestHelper;
using HorCup.Presentation.Players.Commands.AddPlayer;
using HorCup.Presentation.Services.DateTimeService;
using Moq;
using NUnit.Framework;

namespace HorCup.Tests.Players.Commands.AddPlayer
{
	[TestFixture]
	public class AddPlayerCommandValidatorTests
	{
		private AddPlayerCommandValidator _validator;

		[SetUp]
		public void SetUp()
		{
			var dateTimeService = new Mock<IDateTimeService>();
			dateTimeService.Setup(dt => dt.Now).Returns(new DateTime(2018, 1, 1));

			_validator = new AddPlayerCommandValidator(dateTimeService.Object);
		}

		[TestCase(null, "'First Name' must not be empty.")]
		[TestCase("", "'First Name' must not be empty.")]
		[TestCase("More than max length.",
			"The length of 'First Name' must be 20 characters or fewer. You entered 21 characters.")]
		public void AddPlayerCommandValidator_FirstNameIsInvalid_ValidationErrorThrown(string name, string errorMessage)
		{
			var model = new AddPlayerCommand
			{
				FirstName = name
			};

			var result = _validator.TestValidate(model);

			result.ShouldHaveValidationErrorFor(p => p.FirstName)
				.WithErrorMessage(errorMessage);
		}

		[TestCase("Less than max length")]
		[TestCase("Y")]
		public void AddPlayerCommandValidator_FirstNameIsValid_ValidationPassed(string name)
		{
			var model = new AddPlayerCommand
			{
				FirstName = name
			};

			var result = _validator.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(p => p.FirstName);
		}

		[TestCase(null, "'Last Name' must not be empty.")]
		[TestCase("", "'Last Name' must not be empty.")]
		[TestCase("More than max length.",
			"The length of 'Last Name' must be 20 characters or fewer. You entered 21 characters.")]
		public void AddPlayerCommandValidator_LastNameIsInvalid_ValidationErrorThrown(string name, string errorMessage)
		{
			var model = new AddPlayerCommand
			{
				LastName = name
			};

			var result = _validator.TestValidate(model);

			result.ShouldHaveValidationErrorFor(p => p.LastName)
				.WithErrorMessage(errorMessage);
		}

		[TestCase("Less than max length")]
		[TestCase("Y")]
		public void AddPlayerCommandValidator_LastNameIsValid_ValidationPassed(string name)
		{
			var model = new AddPlayerCommand
			{
				LastName = name
			};

			var result = _validator.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(p => p.LastName);
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

		[Test]
		public void AddPlayerCommandValidator_BirthDateIsLessThanMinDate_ValidationErrorThrown()
		{
			var model = new AddPlayerCommand
			{
				BirthDate = new DateTime(1899, 12, 31)
			};

			var result = _validator.TestValidate(model);

			result.ShouldHaveValidationErrorFor(p => p.BirthDate)
				.WithErrorMessage("'Birth Date' must be greater than '1/1/1900 12:00:00 AM'.");
		}

		[Test]
		public void AddPlayerCommandValidator_BirthDateIsBiggerThanMaxDate_ValidationErrorThrown()
		{
			var model = new AddPlayerCommand
			{
				BirthDate = new DateTime(2016, 1, 2)
			};

			var result = _validator.TestValidate(model);

			result.ShouldHaveValidationErrorFor(p => p.BirthDate)
				.WithErrorMessage("'Birth Date' must be less than '1/1/2016 12:00:00 AM'.");
		}

		[Test]
		public void AddPlayerCommandValidator_BirthDateIsValid_ValidationPassed()
		{
			var model = new AddPlayerCommand
			{
				BirthDate = new DateTime(2012, 12, 31)
			};

			var result = _validator.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(p => p.BirthDate);
		}
	}
}