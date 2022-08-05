using FluentValidation;

namespace HorCup.Infrastructure.Queries;

public class SearchQueryBaseValidator : AbstractValidator<SearchQueryBase>
{
	public SearchQueryBaseValidator()
	{
		RuleFor(s => s.Skip)
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(50);

		RuleFor(s => s.Take)
			.GreaterThanOrEqualTo(1)
			.LessThanOrEqualTo(50);
	}
}