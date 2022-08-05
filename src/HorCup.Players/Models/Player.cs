using System;

namespace HorCup.Players.Models;

public class Player
{
	public Guid Id { get; set; }

	public string Nickname { get; set; }
		
	public DateTime Added { get; set; }
	//
	// public ICollection<PlayScore> PlayScores { get; set; }
	//
	// public ICollection<PlayerStatistic> PlayerStatistic { get; set; }
}