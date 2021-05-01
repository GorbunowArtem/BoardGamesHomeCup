using AutoMapper;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.ViewModels;

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