using System;
using System.Collections.Generic;
using HorCup.Games.Models;

namespace HorCup.Games
{
	public static class InMemoryDatabase
	{
		public static readonly Dictionary<Guid, GameDto> Games = new();
		public static readonly List<GameDto> List = new();
	}
}