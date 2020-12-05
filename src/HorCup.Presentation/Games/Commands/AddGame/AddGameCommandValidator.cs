using FluentValidation;

namespace HorCup.Presentation.Games.Commands.AddGame
{
	public class AddGameCommandValidator: AbstractValidator<AddGameCommand>
	{
		public AddGameCommandValidator()
		{
			var constraints = new GamesConstraints();
			
			RuleFor(g => g.Title)
				.NotNull()
				.NotEmpty()
				.MaximumLength(constraints.TitleMaxLength);

			RuleFor(g => g.MaxPlayers)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(constraints.MaxPlayers)
				.GreaterThan(p => p.MinPlayers);
			
			RuleFor(g => g.MinPlayers)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(constraints.MinPlayers)
				.LessThan(p => p.MaxPlayers);
		}
	}
}