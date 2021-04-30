using System;
using MediatR;

namespace HorCup.Players.Players.Commands.AddPlayer
{
	public record AddPlayerCommand : IRequest<Guid>
	{
		public string Nickname { get; set; }

		public DateTime Added { get; set; }
	}
}