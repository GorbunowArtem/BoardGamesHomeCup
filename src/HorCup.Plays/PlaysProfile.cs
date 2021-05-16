using AutoMapper;
using HorCup.Plays.Commands.AddPlay;
using HorCup.Plays.Models;
using HorCup.Plays.Shared.Dto;
using HorCup.Plays.ViewModels;

namespace HorCup.Plays
{
	public class PlaysProfile: Profile
	{
		public PlaysProfile()
		{
			CreateMap<Play, PlayViewModel>();
			CreateMap<Game, GameViewModel>();
			CreateMap<PlayScore, PlayScoreViewModel>();
			
			CreateMap<GameViewModel, Game>();
			CreateMap<AddPlayCommand, Play>();

			CreateMap<PlayScore, PlayerScoresDto>();
		}
	}
}