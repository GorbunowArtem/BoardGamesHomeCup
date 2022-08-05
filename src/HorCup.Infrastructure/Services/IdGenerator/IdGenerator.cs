using System;

namespace HorCup.Infrastructure.Services.IdGenerator;

public class IdGenerator: IIdGenerator
{
	public Guid NewGuid() => Guid.NewGuid();
}