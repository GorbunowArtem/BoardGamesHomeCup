using System;
using FluentValidation;

namespace HorCup.Players.Players.Commands.EditPlayer
{
	public class EditPlayerCommandValidator : AbstractValidator<EditPlayerCommand>
	{
		public EditPlayerCommandValidator()
		{
			var playerConstraints = new PlayerConstraints();

			RuleFor(p => p.Id)
				.NotEqual(Guid.Empty);

			RuleFor(p => p.Nickname)
				.NotNull()
				.NotEmpty()
				.MaximumLength(playerConstraints.MaxNameLength);
		}
	}
}