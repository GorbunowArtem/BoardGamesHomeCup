using AutoMapper;
using HorCup.Players.Models;
using HorCup.Players.Shared.ViewModels;
using PlayerDetailsViewModel = HorCup.Players.ViewModels.PlayerDetailsViewModel;
using PlayerViewModel = HorCup.Players.ViewModels.PlayerViewModel;

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