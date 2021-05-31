using System;
using MediatR;

namespace HorCup.Players.Commands.AddPlayer
{
	public record AddPlayerCommand : IRequest<Guid>
	{
		public string Nickname { get; set; }
	}
}