using System;

namespace HorCup.Infrastructure.Services.DateTimeService
{
	public class DateTimeService: IDateTimeService
	{
		public DateTime Now => DateTime.UtcNow;
	}
}