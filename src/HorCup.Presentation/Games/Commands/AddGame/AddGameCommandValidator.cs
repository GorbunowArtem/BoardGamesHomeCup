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
				.GreaterThanOrEqualTo(0)
				.LessThanOrEqualTo(24);
		}
	}
}