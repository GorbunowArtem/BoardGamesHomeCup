using AutoMapper;
using HorCup.Plays.PlayScores;
using HorCup.Plays.ViewModels;

namespace HorCup.Plays
{
	public class PlaysProfile: Profile
	{
		public PlaysProfile()
		{
			CreateMap<PlayScore, PlayScoreViewModel>();
			CreateMap<Play, PlayViewModel>();
		}
	}
}