using System;
using FluentValidation;

namespace HorCup.Presentation.Plays.Commands.AddPlay
{
	public class AddPlayCommandValidator: AbstractValidator<AddPlayCommand>
	{
		public AddPlayCommandValidator()
		{
			RuleFor(p => p.GameId)
				.NotEqual(Guid.Empty);

			RuleFor(p => p.PlayedDate)
				.GreaterThan(new DateTime(2015, 1, 1))
				.LessThan(DateTime.UtcNow.AddDays(1));
		}
	}
}