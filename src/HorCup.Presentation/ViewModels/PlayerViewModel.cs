using System;

namespace HorCup.Presentation.ViewModels
{
	public class PlayerViewModel
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Nickname { get; set; }
		
		public DateTime BirthDate { get; set; }
	}
}