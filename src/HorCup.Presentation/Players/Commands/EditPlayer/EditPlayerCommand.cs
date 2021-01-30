using System;
using MediatR;

namespace HorCup.Presentation.Players.Commands.EditPlayer
{
	public record EditPlayerCommand(
		Guid Id,
		string FirstName,
		string LastName,
		string Nickname,
		DateTime BirthDate) : IRequest<Unit>;
}