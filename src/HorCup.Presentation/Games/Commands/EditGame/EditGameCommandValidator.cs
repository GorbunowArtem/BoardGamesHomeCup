using System;
using FluentValidation;

namespace HorCup.Presentation.Games.Commands.EditGame
{
	public class EditGameCommandValidator: AbstractValidator<EditGameCommand>
	{
		public EditGameCommandValidator()
		{
			var constraints = new GamesConstraints();

			RuleFor(g => g.Id)
				.NotEqual(Guid.Empty);
			
			RuleFor(g => g.Title)
				.NotNull()
				.NotEmpty()
				.MaximumLength(constraints.TitleMaxLength);

			RuleFor(g => g.MaxPlayers)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(constraints.MaxPlayers)
				.GreaterThanOrEqualTo(p => p.MinPlayers);
			
			RuleFor(g => g.MinPlayers)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(constraints.MinPlayers)
				.LessThanOrEqualTo(p => p.MaxPlayers);
		}
	}
}