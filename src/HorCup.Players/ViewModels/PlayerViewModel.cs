using System;

namespace HorCup.Players.ViewModels
{
	public record PlayerViewModel
	{
		public Guid Id { get; set; }

		public string Nickname { get; set; }
		
		public DateTime Added { get; set; }
	}
}