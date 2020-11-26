using System;
using FluentValidation;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
	{
		public AddPlayerCommandValidator()
		{
			var playerConstraints = new PlayerConstraints();

			RuleFor(p => p.FirstName)
				.NotNull()
				.NotEmpty()
				.MaximumLength(playerConstraints.MaxNameLength);

			RuleFor(p => p.LastName)
				.NotNull()
				.NotEmpty()
				.MaximumLength(playerConstraints.MaxNameLength);

			RuleFor(p => p.Nickname)
				.NotNull()
				.NotEmpty()
				.MaximumLength(playerConstraints.MaxNameLength);
			
			RuleFor(p => p.BirthDate)
				.GreaterThan(playerConstraints.MinBirthDate)
				.LessThan(DateTime.UtcNow);
		}
	}
}