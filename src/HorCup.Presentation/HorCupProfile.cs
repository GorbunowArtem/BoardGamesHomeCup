using AutoMapper;
using HorCup.Presentation.Players;
using HorCup.Presentation.ViewModels;

namespace HorCup.Presentation
{
	public class HorCupProfile: Profile
	{
		public HorCupProfile()
		{
			CreateMap<Player, PlayerViewModel>();
			CreateMap<Player, PlayerDetailsViewModel>();
		}
	}
}