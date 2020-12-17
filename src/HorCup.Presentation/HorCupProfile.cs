﻿using AutoMapper;
using HorCup.Presentation.Games;
using HorCup.Presentation.Players;
using HorCup.Presentation.Plays;
using HorCup.Presentation.PlayScores;
using HorCup.Presentation.ViewModels;

namespace HorCup.Presentation
{
	public class HorCupProfile: Profile
	{
		public HorCupProfile()
		{
			CreateMap<Player, PlayerViewModel>();
			CreateMap<Player, PlayerDetailsViewModel>();
			CreateMap<Game, GameViewModel>();
			CreateMap<Game, GameDetailsViewModel>();
			CreateMap<Play, PlayViewModel>();
			CreateMap<PlayScore, PlayScoreViewModel>();
		}
	}
}