using System;

namespace HorCup.Presentation.ViewModels
{
	public record GameDetailsViewModel : GameViewModel
	{
		public int TimesPlayed { get; set; }

		public double? AverageScore { get; set; }

		public DateTime? LastPlayedDate { get; set; }
	}
}