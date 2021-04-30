using AutoMapper;
using HorCup.Presentation.PlayersStatistic;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.ViewModels;

namespace HorCup.Presentation
{
	public class HorCupProfile : Profile
	{
		public HorCupProfile()
		{
			CreateMap<PlayerStatistic, PlayerStatisticViewModel>();

			
			CreateMap<PlayScore, PlayScoreViewModel>();
			CreateMap<Play, PlayViewModel>();
		}
	}
}