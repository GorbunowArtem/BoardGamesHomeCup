using System;
using FluentAssertions;
using HorCup.Infrastructure.Services.DateTimeService;
using NUnit.Framework;

namespace HorCup.Tests.Services
{
	public class DateTimeServiceTests
	{
		private DateTimeService _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = new DateTimeService();
		}

		[Test]
		public void Now_ReturnsUtcTime()
		{
			var time = _sut.Now;

			time.Date.Should().Be(DateTime.UtcNow.Date);
		}
	}
}