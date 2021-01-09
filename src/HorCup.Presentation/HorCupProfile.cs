using AutoMapper;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.ViewModels;

namespace HorCup.Presentation
{
	public class HorCupProfile : Profile
	{
		public HorCupProfile()
		{
			CreateMap<Player, PlayerViewModel>();
			CreateMap<Player, PlayerDetailsViewModel>();
			CreateMap<Game, GameViewModel>()
				.ForMember(g => g.TimesPlayed, op => op.MapFrom(g => g.GameStatistic.TimesPlayed));
			CreateMap<Play, PlayViewModel>();
			CreateMap<PlayScore, PlayScoreViewModel>();
			CreateMap<Game, GameDetailsViewModel>()
				.ForMember(gd => gd.AverageScore,
					opt => opt.MapFrom(g => g.GameStatistic.AverageScore))
				.ForMember(gd => gd.TimesPlayed,
					opt => opt.MapFrom(g => g.GameStatistic.TimesPlayed))
				.ForMember(gd => gd.LastPlayedDate,
					opt => opt.MapFrom(g => g.GameStatistic.LastPlayedDate));
		}
	}
}