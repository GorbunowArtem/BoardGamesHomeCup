using System;

namespace HorCup.Presentation.Services.IdGenerator
{
	public class IdGenerator: IIdGenerator
	{
		public Guid NewGuid() => Guid.NewGuid();
	}
}