using System;
using System.Collections.Generic;
using HorCup.Infrastructure.ViewModels;
using HorCup.Plays.Models;
using MediatR;

namespace HorCup.Plays.Commands.AddPlay
{
	public record AddPlayCommand : IRequest<Guid>
	{
		public Guid Id { get; set; }

		public IdName Game { get; set; }

		public DateTime PlayedDate { get; set; }

		public string Notes { get; set; }

		public IEnumerable<PlayScore> PlayerScores { get; set; }
	}
}