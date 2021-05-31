using System;

namespace HorCup.Infrastructure.Services.DateTimeService
{
	public interface IDateTimeService
	{
		DateTime Now { get; }
	}
}