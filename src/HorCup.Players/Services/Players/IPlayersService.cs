using System;
using System.Threading.Tasks;

namespace HorCup.Players.Services.Players
{
	public interface IPlayersService
	{
		Task<bool> IsNicknameUniqueAsync(string nickname, Guid? id);
	}
}