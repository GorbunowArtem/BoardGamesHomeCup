using System;
using FluentAssertions;
using HorCup.Presentation.Services.IdGenerator;
using NUnit.Framework;

namespace HorCup.Tests.Services
{
	public class IdGeneratorTests
	{
		private IdGenerator _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new IdGenerator();
		}

		[Test]
		public void NewGuid_ShouldReturnNewGuid()
		{
			var result = _sut.NewGuid();

			result.GetType().Should().Be(typeof(Guid));
		}
	}
}