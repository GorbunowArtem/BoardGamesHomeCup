using System;
using FluentValidation.TestHelper;
using HorCup.Presentation.Plays.Commands.AddPlay;
using HorCup.Presentation.Services.DateTimeService;
using Moq;
using NUnit.Framework;

namespace HorCup.Tests.Plays.Commands.AddPlay
{
	[TestFixture]
	public class AddPlayCommandValidatorTests
	{
		private AddPlayCommandValidator _validator;
		private Mock<IDateTimeService> _dateTimeServiceMock;

		[SetUp]
		public void SetUp()
		{
			_dateTimeServiceMock = new Mock<IDateTimeService>();
			_dateTimeServiceMock.Setup(dt => dt.Now).Returns(TestExtensions.ToDateTime(2020, 1, 1));
			_validator = new AddPlayCommandValidator(_dateTimeServiceMock.Object);
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

		[Test]
		public void AddPlayCommandValidator_PlayedDateIsLessThanMinimumValue_ValidationErrorThrown()
		{
			var model = new AddPlayCommand
			{
				PlayedDate = new DateTime(2014, 12, 31)
			};

			var result = _validator.TestValidate(model);

			result.ShouldHaveValidationErrorFor(pd => pd.PlayedDate)
				.WithErrorMessage("'Played Date' must be greater than '1/1/2015 12:00:00 AM'.");
		}

		[Test]
		public void AddPlayCommandValidator_PlayedMinDateIsValid_ValidationPassed()
		{
			var model = new AddPlayCommand
			{
				PlayedDate = new DateTime(2015, 1, 2)
			};

			var result = _validator.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(pd => pd.PlayedDate);
		}
	}
}