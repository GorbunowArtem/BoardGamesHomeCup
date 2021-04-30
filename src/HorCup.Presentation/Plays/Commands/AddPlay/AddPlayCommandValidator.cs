using System;
using FluentValidation;
using HorCup.Infrastructure.Services.DateTimeService;

namespace HorCup.Presentation.Plays.Commands.AddPlay
{
	public class AddPlayCommandValidator: AbstractValidator<AddPlayCommand>
	{
		public AddPlayCommandValidator(IDateTimeService dateTimeService)
		{
			RuleFor(p => p.GameId)
				.NotEqual(Guid.Empty);

			RuleFor(p => p.PlayedDate)
				.GreaterThan(new DateTime(2015, 1, 1))
				.LessThanOrEqualTo(dateTimeService.Now);
		}
	}
}