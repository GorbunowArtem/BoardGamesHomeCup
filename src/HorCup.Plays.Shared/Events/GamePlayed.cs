using System;
using System.Collections.Generic;
using HorCup.Plays.Shared.Dto;

namespace HorCup.Plays.Shared.Events;

public class GamePlayed
{
	public Guid GameId { get; set; }
		
	public double AverageScore { get; set; }

	public int TimesPlayed { get; set; }

	public DateTime LastPlayedDate { get; set; }
		
	public IEnumerable<PlayerScoresDto> PlayedScores { get; set; }
}