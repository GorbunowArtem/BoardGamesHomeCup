using AutoMapper;
using HorCup.Games.Models;
using HorCup.Games.ViewModels;

namespace HorCup.Games
{
	public class GamesProfile : Profile
	{
		public GamesProfile()
		{
			CreateMap<GameAggregate, GameViewModel>();
			CreateMap<GameAggregate, GameDetailsViewModel>();
		}
	}
}