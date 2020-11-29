using System;
using MediatR;

namespace HorCup.Presentation.Players.Commands.DeletePlayer
{
	public class DeletePlayerCommand: IRequest<Unit>
	{
		public Guid Id { get; }

		public DeletePlayerCommand(Guid id)
		{
			Id = id;
		}
	}
}