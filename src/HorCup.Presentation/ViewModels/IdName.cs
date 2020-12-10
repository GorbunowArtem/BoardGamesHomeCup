using System;

namespace HorCup.Presentation.ViewModels
{
	public class IdName
	{
		public IdName(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
		
		public Guid Id { get; }

		public string Name { get; }
	}
}