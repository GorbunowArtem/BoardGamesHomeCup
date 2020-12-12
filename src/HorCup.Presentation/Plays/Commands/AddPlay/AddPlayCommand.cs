using System;
using System.Collections.Generic;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Plays.Commands.AddPlay
{
	public class AddPlayCommand : IRequest<Unit>
	{
		public Guid Id { get; set; }
		
		public Guid GameId { get; set; }

		public string Notes { get; set; }

		public IEnumerable<PlayScoreViewModel> PlayerScores { get; set; }

		public DateTime PlayedDate { get; set; }
	}
}