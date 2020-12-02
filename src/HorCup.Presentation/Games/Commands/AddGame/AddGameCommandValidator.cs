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
				.LessThanOrEqualTo(24)
				.LessThanOrEqualTo(p => p.MinPlayers);
			
			RuleFor(g => g.MinPlayers)
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(20)
				.LessThanOrEqualTo(p => p.MaxPlayers);
		}
	}
}