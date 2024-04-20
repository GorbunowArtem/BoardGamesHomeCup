using AutoMapper;
using HorCup.Players.Models;
using HorCup.Players.ViewModels;

namespace HorCup.Players
{
	public class PlayersProfile : Profile
	{
		public PlayersProfile()
		{
			CreateMap<Player, PlayerViewModel>();
			CreateMap<Player, PlayerDetailsViewModel>();
		}
	}
}