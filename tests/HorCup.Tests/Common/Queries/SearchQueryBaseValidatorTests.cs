using FluentValidation.TestHelper;
using HorCup.Infrastructure.Queries;
using NUnit.Framework;

namespace HorCup.Tests.Common.Queries
{
	[TestFixture]
	public class SearchQueryBaseValidatorTests
	{
		private SearchQueryBaseValidator _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new SearchQueryBaseValidator();
		}

		[TestCase(-1)]
		[TestCase(0)]
		[TestCase(51)]
		public void SearchQueryBaseValidator_TakeIsInvalid_ValidationErrorThrown(int take)
		{
			var model = new SearchQueryBase
			{
				Take = take
			};

			var result = _sut.TestValidate(model);

			result.ShouldHaveValidationErrorFor(t => t.Take);
		}

		[TestCase(1)]
		[TestCase(5)]
		[TestCase(50)]
		public void SearchQueryBaseValidator_TakeIsValid_ValidationPassed(int take)
		{
			var model = new SearchQueryBase
			{
				Take = take
			};

			var result = _sut.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(t => t.Take);
		}

		[TestCase(-1)]
		[TestCase(51)]
		public void SearchQueryBaseValidator_SkipIsInvalid_ValidationErrorThrown(int skip)
		{
			var model = new SearchQueryBase
			{
				Skip = skip
			};

			var result = _sut.TestValidate(model);

			result.ShouldHaveValidationErrorFor(t => t.Skip);
		}

		[TestCase(1)]
		[TestCase(5)]
		[TestCase(50)]
		public void SearchQueryBaseValidator_SkipIsValid_ValidationPassed(int skip)
		{
			var model = new SearchQueryBase
			{
				Skip = skip
			};

			var result = _sut.TestValidate(model);

			result.ShouldNotHaveValidationErrorFor(t => t.Skip);
		}
	}
}