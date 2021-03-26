﻿using FluentValidation;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
	{
		public AddPlayerCommandValidator()
		{
			var playerConstraints = new PlayerConstraints();

			RuleFor(p => p.Nickname)
				.NotNull()
				.NotEmpty()
				.MaximumLength(playerConstraints.MaxNameLength);
		}
	}
}