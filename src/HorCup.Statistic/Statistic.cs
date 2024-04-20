using AutoMapper;
using HorCup.Statistic.PlayersStatistic;
using HorCup.Statistic.ViewModels;

namespace HorCup.Statistic
{
	public class Statistic : Profile
	{
		public Statistic()
		{
			CreateMap<PlayerStatistic, PlayerStatisticViewModel>();

		}
	}
}