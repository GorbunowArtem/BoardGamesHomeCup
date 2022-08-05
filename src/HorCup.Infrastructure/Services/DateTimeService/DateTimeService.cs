using System;

namespace HorCup.Infrastructure.Services.DateTimeService;

public class DateTimeService: IDateTimeService
{
	public DateTime UtcNow => DateTime.UtcNow;
}