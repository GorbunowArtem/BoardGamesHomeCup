using System;

namespace HorCup.Presentation.Services.DateTimeService
{
	public class DateTimeService: IDateTimeService
	{
		public DateTime Now => DateTime.UtcNow;
	}
}