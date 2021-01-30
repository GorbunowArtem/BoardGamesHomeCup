using FluentValidation;
using HorCup.Presentation.Services.DateTimeService;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
	{
		public AddPlayerCommandValidator(IDateTimeService dateTimeService)
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
				.LessThan(dateTimeService.Now.AddYears(-2));
		}
	}
}