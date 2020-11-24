using System;

namespace HorCup.Presentation.Services.IdGenerator
{
	public interface IIdGenerator
	{
		Guid Generate();
	}
}