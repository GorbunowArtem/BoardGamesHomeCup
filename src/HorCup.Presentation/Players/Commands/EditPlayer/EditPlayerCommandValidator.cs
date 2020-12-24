using System;
using FluentValidation;

namespace HorCup.Presentation.Players.Commands.EditPlayer
{
	public class EditPlayerCommandValidator : AbstractValidator<EditPlayerCommand>
	{
		public EditPlayerCommandValidator()
		{
			var playerConstraints = new PlayerConstraints();

			RuleFor(p => p.Id)
				.NotEqual(Guid.Empty);
			
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