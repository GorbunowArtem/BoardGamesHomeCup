using System;

namespace HorCup.Presentation.ViewModels
{
	public class GameViewModel
	{
		public Guid Id { get; set; }

		public string Title { get; set; }
		
		public int MaxPlayers { get; set; }
		
		public int MinPlayers { get; set; }
	}
}