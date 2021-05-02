// using System;
// using System.Collections.Generic;
// using HorCup.Presentation.PlayScores;
// using MediatR;
//
// namespace HorCup.Presentation.GamesStatistic.Commands.GamePlayed
// {
// 	public record GamePlayedNotification(
// 		Guid GameId,
// 		double? AverageScore,
// 		DateTime LastPlayedDate,
// 		IEnumerable<PlayScore> PlayerScore
// 	) : INotification;
// }