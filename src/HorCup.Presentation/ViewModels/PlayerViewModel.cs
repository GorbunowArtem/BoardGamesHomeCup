using System;

namespace HorCup.Presentation.ViewModels
{
	public record PlayerViewModel
	{
		public Guid Id { get; set; }

		public string Nickname { get; set; }
		
		public DateTime Added { get; set; }
	}
}