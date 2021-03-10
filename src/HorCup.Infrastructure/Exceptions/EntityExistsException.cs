using System;

namespace HorCup.Infrastructure.Exceptions
{
	public class EntityExistsException: Exception
	{
		public EntityExistsException(string name, object key): base($"Entity {name} with {key} already exists")
		{
			
		}
	}
}