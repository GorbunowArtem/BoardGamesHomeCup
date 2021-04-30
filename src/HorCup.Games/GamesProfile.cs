using AutoMapper;
using HorCup.Games.Models;
using HorCup.Games.ViewModels;

namespace HorCup.Games
{
	public class GamesProfile : Profile
	{
		public GamesProfile()
		{
			CreateMap<Game, GameViewModel>();
				// .ForMember(g => g.TimesPlayed, op => op.MapFrom(g => g.GameStatistic.TimesPlayed));
			CreateMap<Game, GameDetailsViewModel>();
				// .ForMember(gd => gd.AverageScore,
				// 	opt => opt.MapFrom(g => g.GameStatistic.AverageScore))
				// .ForMember(gd => gd.TimesPlayed,
				// 	opt => opt.MapFrom(g => g.GameStatistic.TimesPlayed))
				// .ForMember(gd => gd.LastPlayedDate,
				// 	opt => opt.MapFrom(g => g.GameStatistic.LastPlayedDate));
		}
	}
}