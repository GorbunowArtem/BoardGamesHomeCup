using System;
using HorCup.Presentation.ViewModels;
using MediatR;

namespace HorCup.Presentation.Players.Commands.AddPlayer
{
	public class AddPlayerCommand : IRequest<PlayerViewModel>
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Nickname { get; set; }
		
		public DateTime BirthDate { get; set; }
	}
}