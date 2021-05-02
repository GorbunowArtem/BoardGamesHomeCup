using System;
using System.Collections.Generic;
using HorCup.Plays.ViewModels;
using MediatR;

namespace HorCup.Plays.Commands.AddPlay
{
	public record AddPlayCommand : IRequest<Guid>
	{
		public Guid GameId { get; set; }

		public string Notes { get; set; }

		public IEnumerable<PlayScoreViewModel> PlayerScores { get; set; }

		public DateTime PlayedDate { get; set; }
	}
}