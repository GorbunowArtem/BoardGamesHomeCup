using System;

namespace HorCup.Infrastructure.Services.IdGenerator
{
	public interface IIdGenerator
	{
		Guid NewGuid();
	}
}