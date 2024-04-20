using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation.Results;

namespace HorCup.Infrastructure.Exceptions
{
	[ExcludeFromCodeCoverage]
	public class ValidationException: Exception
	{
		public ValidationException()
			: base("One or more validation failures have occurred.")
		{
			Failures = new Dictionary<string, string[]>();
		}

		public ValidationException(IReadOnlyCollection<ValidationFailure> failures)
			: this()
		{
			var propertyNames = failures
				.Select(e => e.PropertyName)
				.Distinct<string>();

			foreach (var propertyName in propertyNames)
			{
				var propertyFailures = failures
					.Where(e => e.PropertyName == propertyName)
					.Select(e => e.ErrorMessage)
					.ToArray<string>();

				Failures.Add(propertyName, propertyFailures);
			}
		}

		public IDictionary<string, string[]> Failures { get; }
	}
}