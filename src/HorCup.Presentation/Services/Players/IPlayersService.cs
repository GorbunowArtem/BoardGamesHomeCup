using System;
using System.Threading.Tasks;

namespace HorCup.Presentation.Services.Players
{
	public interface IPlayersService
	{
		Task<bool> IsNicknameUniqueAsync(string nickname, Guid? id);
	}
}