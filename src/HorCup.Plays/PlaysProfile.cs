using AutoMapper;
using HorCup.Plays.Commands.AddPlay;
using HorCup.Plays.Models;
using HorCup.Plays.ViewModels;

namespace HorCup.Plays
{
	public class PlaysProfile: Profile
	{
		public PlaysProfile()
		{
			CreateMap<Play, PlayViewModel>();
			CreateMap<AddPlayCommand, Play>();
		}
	}
}